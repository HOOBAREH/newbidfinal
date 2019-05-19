using DataLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class AuctionLogsRepo: IAuctionLogRepo
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
        public List<AuctionLogs> GetLast8ByAuctionId(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.AuctionLogs.Where(x => x.AuctionId == id).Take(8).ToList();
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
                    UserId = model.UserId
                };
                db.AuctionLogs.Add(auction);
                db.SaveChanges();
            }
        }
    }
}