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
using System.Web.Script.Serialization;

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
                var exixts = _userRepo.UsernameExists(model.Email, model.Mobile);
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
            //var currentUser = _userRepo.GetUserById(int.Parse(User.Identity.Name));
            //if (currentUser.RealBid != 0)
            //{
            //    var user = _userRepo.LowerBids(int.Parse(User.Identity.Name));
            //    if (user == null)
            //    {
            //        ViewBag.Error = "لطفا مجددا تلاش کنید";
            //        return Json(new
            //        {
            //            result = false
            //        });
            //    }
            //    else
            //    {
            //        var auction = _auctionRepo.GetAuctionById(model.id);
            //        auction = _auctionRepo.UpdateWithClick(auction, user.Id, auction.Reserve_Price ?? 0);
            //        if (auction == null)
            //        {
            //            return Json(new
            //            {
            //                result = "fail"
            //            });
            //        }
            //        var modellog = new AuctionLogs
            //        {
            //            AuctionId = model.id,
            //            TypeBid = 1,
            //            UserId = user.Id,
            //            Price = auction.Reserve_Price
            //        };
            //        new AuctionLogsRepo().Insert(modellog);
            //        return Json(auction.Reserve_Price, JsonRequestBehavior.AllowGet);

            //    }
            //}
            //else
            return Json(new
            {
                result = "Low"
            });
        }
        public ActionResult GetAuctionLog(int auctionId)
        {
            var list = new AuctionLogsRepo().GetLast8ByAuctionId(auctionId);
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
        [HttpPost]
        [Authorize]

        public ActionResult UpdateTimer()
        {
            var auctions = _auctionRepo.GetAuctions().Where(x=>!x.IsClose);
            List<AuctionModel> list = new List<AuctionModel>();
            List<string> listStr = new List<string>();
            foreach (var item in auctions)
            {
                var auction = item;
                bool result = false, startStatus = auction.StartStatus, isclose = auction.IsClose;
                TimeSpan timer = new TimeSpan();
                TimeSpan time = new TimeSpan();
                string colorStatus = "Black";

                var status = auction.Auction_Time.CompareTo(new TimeSpan(0, 0, 0));
                timer = auction.Auction_Time;
                if (!auction.IsClose)
                {
                    if (timer.CompareTo(new TimeSpan(0, 0, 0)) == 0)
                    {
                        time = timer;
                    }
                    else
                    {
                        time = timer.Subtract(TimeSpan.FromSeconds(1));
                    }
                    if (time.CompareTo(new TimeSpan(0, 0, 0)) == 0)//if time==0
                    {
                        if (!auction.StartStatus)
                        {
                            startStatus = true;
                            time = TimeSpan.FromSeconds(auction.Close_Time);
                        }
                        else
                        {
                            isclose = true;
                        }
                    }

                    auction = _auctionRepo.UpdateTimer2(auction.Id, time, startStatus, isclose);

                }
                colorStatus = startStatus == true ? "Red" : "Black";
                var model = new AuctionModel
                {
                    Auction_Time = auction.Auction_Time,
                    Close_Time = auction.Close_Time,
                    CurrentBid_Id = auction.CurrentBid_Id,
                    Current_UserId = auction.Current_UserId,
                    Reserve_Price = auction.Reserve_Price,
                    IsActive = auction.IsActive,
                    id = auction.Id,
                    ProductId = auction.ProductId,
                    User = auction.User,
                    colorStatus = colorStatus,
                    IsClose=auction.IsClose,
                    Product=auction.Product
                };
                list.Add(model);
                //var r = JsonConvert.SerializeObject(model);
              //listStr.Add( new JavaScriptSerializer().Serialize(r));
            }

            //return Json(new
            //{
            //    result = list
            //},JsonRequestBehavior.AllowGet);

            //var res = false;
            //if (res)
            //{
            //    return Json(new
            //    {
            //        result = true
            //    });
            //}
            //else
            return PartialView("_ListAuction", list);
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