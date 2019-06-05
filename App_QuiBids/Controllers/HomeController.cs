using App_QuiBids.Models;
using DataLayer.Repository;
using DataLayer.IRepository;
using DataLayer.Utilites;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace App_QuiBids.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IAuctionRepo _auctionRepo;
        SessionContext context = new SessionContext();


        public HomeController() : this(new UserRepo(), new AuctionRepo())
        {
        }

        public HomeController(IUserRepo userRepo, IAuctionRepo auctionRepo)
        {
            _userRepo = userRepo;
            _auctionRepo = auctionRepo;
        }


        // GET: Home
        //[Authorize]
        public ActionResult Index(int id = 0)
        {
            var user = User.Identity.Name;
            var auctions = _auctionRepo.GetAuctions();
            if (id != 0)
            {

                var price = auctions.FirstOrDefault().Reserve_Price + 1;
                return Json(new
                {
                    currentPrice = price
                });
            }
            else
                return View(auctions);


        }
        public ActionResult Login()
        {
            return View("Register");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var hash = new Helpers().Encryption(model.Password);
                var res = _userRepo.Login(model.UserName, hash);
                if (res != null)
                {

                    context.SetAuthenticationToken(res.Id.ToString(), false, res);
                    _userRepo.LastLogin(model.UserId);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "نام کاربری یا رمز اشتباه است";
                    return View("Register", model);
                }
            }

            return View();
        }
        [Authorize]

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session["Admin"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var pass = new Helpers().Encryption(model.Password);
                var exixts = _userRepo.UsernameExists(model.Email);
                if (!exixts)
                {
                    var user = new User()
                    {
                        Fname = model.Fname,
                        Lname = model.Lname,
                        Email = model.Email,
                        Address = model.Address,
                        Mobile = model.Mobile,
                        LastLogin = DateTime.Now,
                        Password = pass,
                        RealBid = 0,
                        VoucherBid = 0,
                        HideLocation = false
                    };
                    var register = _userRepo.Register(user);
                    if (register)
                    {
                        return RedirectToAction("Login");
                    }
                    ViewBag.Error = "ثبت اطلاعات شما با مشکل مواجه شد. لطفا از صحت اطلاعات اطمینان حاصل کنید و مجددا تلاش نمایید.";
                    return View(model);
                }
                else
                {
                    ViewBag.Error = "این ایمیل در سیستم موجود می باشد.";
                    return View(model);
                }
            }
            ViewBag.Error = "لطفا از صحت اطلاعات اطمینان حاصل کنید و مجددا تلاش نمایید.";
            return View(model);
        }
        [Authorize]

        public ActionResult LowerBids(AuctionModel model)
        {
            var auctions = _auctionRepo.GetAuctionById(model.id);
            _auctionRepo.UpdateWithClick(auctions, model.Current_UserId, model.Reserve_Price);
            var user = _userRepo.LowerBids(model.Current_UserId);
            if (user == null)
            {
                ViewBag.Error = "لطفا مجددا تلاش کنید";
                return Json(new
                {
                    result = false
                });
            }
            else
            {
                var modellog = new AuctionLogs
                {
                    AuctionId = model.id,
                    TypeBid = 1,
                    UserId = model.Current_UserId,
                    Price = model.Reserve_Price
                };
                new AuctionLogsRepo().Insert(modellog);
                var list = new AuctionLogsRepo().GetLast8ByAuctionId(model.id);
                var repoList = JsonConvert.SerializeObject(list,
                              Formatting.None,
                              new JsonSerializerSettings()
                              {
                                  ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                              });
                if (list != null)
                {
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail");
                }
            }
            //}
            //else
            //return View();
        }
        [HttpPost]
        [Authorize]

        public ActionResult UpdateTimer(int id, TimeSpan timer, bool startStatus)
        {
            var res = _auctionRepo.UpdateTimer(id, timer, startStatus);
            if (res)
            {
                return Json(new
                {
                    result = true
                });
            }
            else
                return Json(new
                {
                    result = false
                });
        }
        [Authorize]

        public ActionResult UpdateIsclose(int id)
        {
            var auction = _auctionRepo.UpdateIsclose(id);
            Session["Auction"] = auction;
            if (auction != null)
            {
                return Json(new
                {
                    result = true
                });
            }
            else
                return Json(new
                {
                    result = false
                });
        }
        /*--------------------------------------------*/
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Games()
        {
            return View();
        }
        public ActionResult Badges()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Action(int id)
        {
            var s = _auctionRepo.GetProductByAuction(id);
            var auction = _auctionRepo.GetAuctionById(id);
            return View(auction);
        }
        public ActionResult Auction(int id)
        {
            var auction = _auctionRepo.GetProductByAuction(id);

            //var auction = _auctionRepo.GetAuctionById(id);
            Session["Auction"] = auction;
            return View(auction);
        }
        public ActionResult Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProfile()
        {
            return View();
        }
    }
}