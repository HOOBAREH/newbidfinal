using DataLayer.IRepository;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_QuiBids.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;
        public AccountController() : this(new UserRepo())
        {
        }
        public AccountController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login","Home");
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
                var User = Session["Admin"];
                return View(User);
            }
        }
        public ActionResult MyBadges()
        {
            return View();
        }
     
    }
}