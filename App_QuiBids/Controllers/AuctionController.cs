using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer.IRepository;
using DataLayer;
using App_QuiBids.Models;
using DataLayer.Models;

namespace App_QuiBids.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionLogRepo _logRepo;
        private readonly IAuctionRepo _auctionRepo;
        public AuctionController()
        {
            _auctionRepo = new AuctionRepo();
            _logRepo = new AuctionLogsRepo();
        }

        // GET: Auction
        public ActionResult Index(int id)
        {
            var auction = _auctionRepo.GetProductByAuction(id);
            AuctionListModel model = new AuctionListModel()
            {
                Auctions = auction,
                statisticsList = _auctionRepo.GetStatistics()
            };



            return View(model);
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
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult UpdateTimer(int id)
        {
            var auction = _auctionRepo.GetProductByAuction(id);
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
                    if (timer.CompareTo(new TimeSpan(0, 0, 0)) == 0)
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
                }

                result = _auctionRepo.UpdateTimer(id, time, startStatus, isclose);
            }
            colorStatus = startStatus == true ? "Red" : "Black";
            if (result)
            {
                return Json(new
                {
                    startStatus = startStatus,
                    isclose,
                    result = (new TimeSpan(time.Hours, time.Minutes, time.Seconds)).ToString()
                });
            }
            else
                return Json(false);
        }

        /// <summary>
        /// Kam kardane bids va sabte gheimate:
        /// 1-Check bids 2-check start shodane time mozaede  3-kam kardan bid  4-sabte gheimat. 5 sabte log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult ParticipateInAuctions(int id)
        {
            var bid = new UserRepo().GetBidsById(int.Parse(User.Identity.Name));
            if (bid != 0)
            {
                var Uid = int.Parse(User.Identity.Name);
                var auction = _auctionRepo.GetAuctionById(id);
                if (auction.StartStatus && !auction.IsClose)
                {
                    if (auction.Current_UserId == Uid)
                    {
                        ViewBag.Error = "شما قادر نیستید،دو پیشنهاد را پشت سر هم بدهید";
                        return Json(
                             "notAllowed"
                        );
                    }
                    var user = new UserRepo().GetUserById(int.Parse(User.Identity.Name));
                    var result = _auctionRepo.ParticipateInAuctions(id, user.Id);

                    if (result == 0)
                    {
                        ViewBag.Error = "لطفا مجددا تلاش کنید";
                        return Json("Try"
                        );
                    }
                    else
                    {
                        return Json(new
                        {
                            name = user.Fname,
                            price = result
                        }
                            , JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json("NoBid"
            );
        }

        public ActionResult GetEight(int id)
        {
            var list = _logRepo.GetEight(id);
            return Json(new
            {
                result = list
            });
        }
        public ActionResult Statistics()
        {
            var result = _auctionRepo.GetStatistics();
            return View();
        }
        public ActionResult CheckStatus(int id)
        {
            return Json(_auctionRepo.CheckStatus(id));
        }
    }
}