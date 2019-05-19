using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer.IRepository;
using DataLayer;

namespace App_QuiBids.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionLogRepo _logRepo;
        public AuctionController(IAuctionLogRepo logRepo)
        {
            _logRepo = logRepo;
        }
        // GET: Auction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToAcuctionLog(int auctionId, byte type)
        {
            if (Session["Admin"] == null)
            {
                var user = (User)Session["Admin"];
                var model = new AuctionLogs
                {
                    AuctionId = auctionId,
                    //TypeBid=
                    UserId = user.Id
                };
                _logRepo.Insert(model);
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}