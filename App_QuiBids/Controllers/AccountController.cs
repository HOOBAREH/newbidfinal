using App_QuiBids.Models;
using DataLayer;
using DataLayer.IRepository;
using DataLayer.Repository;
using DataLayer.Utilites;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_QuiBids.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly ICountryRepo _countryRepo;
        public AccountController() : this(new UserRepo(), new CountryRepo())
        {
        }
        public AccountController(IUserRepo userRepo, ICountryRepo countryRepo)
        {
            _userRepo = userRepo;
            _countryRepo = countryRepo;
        }

        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var User = Session["Admin"];
                return View(User);
            }
        }
        // GET: Account
        public ActionResult MyGames()
        {
            return View();
        }
        public ActionResult NewBids()
        {
            return View();
        }
        public ActionResult MyAccount()
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var UserModel = (User)Session["Admin"];
                var user = _userRepo.GetUserById(UserModel.Id);
                Session["Admin"] = user;
                return View(user);
            }
        }
        public ActionResult MyBadges()
        {
            return View();
        }
        public ActionResult GetCountries()
        {
            return Json(_countryRepo.GetCountries().Select(x => new
            {
                countryID = x.Id,
                countryName = x.Country_name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateProfile(UserModel userModel)
        {
            if (userModel != null)
            {
                _userRepo.UpdateProfile(userModel);
                return RedirectToAction("MyAccount");
            }
            else
                return RedirectToAction("MyAccount");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldpassword, string password)
        {
            if (Session["Admin"] != null)
            {
                var user = (User)Session["Admin"];
                var oldPassHash = new Helpers().Encryption(oldpassword);
                if (oldPassHash == user.Password)
                {
                    var newPassHash = new Helpers().Encryption(password);
                    var result = _userRepo.ChangePass(newPassHash, user.Id);
                    if (result)
                    {
                        Session["Admin"] = null;
                        ViewBag.Message = "رمز عبور شما با موفقیت نغییر کرد";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error = "عملیات با خطا مواجه شد. لطفا مجدد تلاش فرمایید.";
                    }
                }
                return View();
            }
            else
                return RedirectToAction("Login", "Home");


        }
        public ActionResult MyInformation()
        {
            return View();
        }
        public ActionResult MyAvatar()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
        public ActionResult RedeemPromo()
        {
            return View();
        }
        public ActionResult UpdateImage(string imageName)
        {
            var UserModel = (User)Session["Admin"];
            var filename = Path.GetFileName(imageName + ".png");
            var path = Path.Combine(Server.MapPath("~/Content/img/avatar/"), filename);
            var user = _userRepo.UpdateImage(UserModel.Id, path);
            Session["Admin"] = user;
            return RedirectToAction("MyAvatar");
        }
    }
}