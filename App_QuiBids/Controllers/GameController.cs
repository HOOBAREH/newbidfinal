using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_QuiBids.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pacman()
        {
            return View();
        }
        public ActionResult FloppyBird()
        {
            return View();
        }
        public ActionResult TicTac()
        {
            return View();
        }
        public ActionResult AddBid()
        {
            var result = new UserRepo().AddToBids(int.Parse(User.Identity.Name));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}