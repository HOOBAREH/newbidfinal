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
    [Authorize]
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
            var user = _userRepo.GetUserById(int.Parse(User.Identity.Name));
            return View(user);
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
            var user = _userRepo.GetUserById(int.Parse(User.Identity.Name));
            return View(user);
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

            var user = _userRepo.GetUserById(int.Parse(User.Identity.Name));
            var oldPassHash = new Helpers().Encryption(oldpassword);
            if (oldPassHash == user.Password)
            {
                var newPassHash = new Helpers().Encryption(password);
                var result = _userRepo.ChangePass(newPassHash, user.Id);
                if (result)
                {
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
            var UserModel = _userRepo.GetUserById(int.Parse(User.Identity.Name));

            var user = _userRepo.UpdateImage(UserModel.Id, imageName + ".png");
            return RedirectToAction("MyAvatar");
        }
        //public ActionResult RecentParticipation(int id)
        //{

        //}
    }
}