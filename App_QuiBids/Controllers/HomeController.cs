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

namespace App_QuiBids.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IAuctionRepo _auctionRepo;

        public HomeController() : this(new UserRepo(), new AuctionRepo())
        {
        }

        public HomeController(IUserRepo userRepo, IAuctionRepo auctionRepo)
        {
            _userRepo = userRepo;
            _auctionRepo = auctionRepo;
        }


        // GET: Home
        public ActionResult Index(int id = 0)
        {
            //Session["Admin"] = _userRepo.GetUserById(2);
            if (Session["Admin"] == null)
            {
                Session["Admin"] = "";
            }
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
                    Session["Admin"] = res;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "نام کاربری یا رمز اشتباه است";
                    return View();
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
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
                        Password = pass
                    };
                    var register = _userRepo.Register(user);
                    if (register)
                    {
                        Session["Admin"] = user;
                        return RedirectToAction("Index");
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
        public ActionResult LowerBids(AuctionModel model)
        {
            var auctions = _auctionRepo.GetAuctionById(model.id);
            _auctionRepo.UpdateWithClick(auctions, model.Current_UserId, model.Reserve_Price);
            var bid = _userRepo.LowerBids(model.Current_UserId);
            if (bid == null)
            {
                ViewBag.Error = "لطفا مجددا تلاش کنید";
                return Json(new
                {
                    result = false
                });
            }
            else
            {
                Session["Admin"] = bid;
                return Json(new
                {
                    currentbids = bid.RealBid
                    //currentPrice = bid
                });
            }
            //}
            //else
            //return View();
        }
        [HttpPost]
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
        public ActionResult UpdateIsclose(int id)
        {
            var res = _auctionRepo.UpdateIsclose(id);
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
        public ActionResult Abuot()
        {
            return View();
        }
        public ActionResult Action(int id)
        {
            var auction = _auctionRepo.GetAuctionById(id);
            return View(auction);
        }
    }
}