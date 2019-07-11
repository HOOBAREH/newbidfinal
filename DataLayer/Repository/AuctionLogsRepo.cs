using DataLayer.IRepository;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class AuctionLogsRepo : IAuctionLogRepo
    {
        public List<AuctionLogs> GetAll()
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.AuctionLogs.ToList();
            }
        }
        public AuctionLogs GetByUserId(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.AuctionLogs.Where(x => x.UserId == id).FirstOrDefault();
            }
        }
        public AuctionLogs GetByAuctionId(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.AuctionLogs.Where(x => x.AuctionId == id).FirstOrDefault();
            }
        }
        public List<LogModel> GetEight(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {

                var last = db.AuctionLogs.Include("Auction").Include("User").Where(x => x.AuctionId == id).OrderByDescending(x => x.Id).Take(9).Select(x => new LogModel
                {
                    Image = x.User.Image,
                    Price = x.Price.Value,
                    UserName = x.User.Fname,
                }).OrderBy(x=>x.Price).ToList();
                return last;
            }
        }

        public LogModel GetLast8ByAuctionId(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {

                var last = db.AuctionLogs.Include("Auction").Include("User").Where(x => x.AuctionId == id).OrderByDescending(x => x.Id).FirstOrDefault();

                var log = new LogModel
                {
                    Image = last.User.Image,
                    Price = last.Price.Value,
                    UserName = last.User.Fname,

                };
                return log;

            }
        }
        public void Insert(AuctionLogs model)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                AuctionLogs auction = new AuctionLogs
                {
                    AuctionId = model.AuctionId,
                    DateTime = DateTime.Now,
                    TypeBid = model.TypeBid,
                    UserId = model.UserId,
                    Price = model.Price
                };
                db.AuctionLogs.Add(auction);
                db.SaveChanges();
            }
        }
    }
}