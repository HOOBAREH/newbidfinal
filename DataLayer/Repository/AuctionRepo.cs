using DataLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class AuctionRepo : IAuctionRepo
    {
        public AuctionRepo()
        {

        }

        public List<Auction> GetAuctions()
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Auction.Include("Product").Include("User").ToList();
            }
        }
        public Auction GetAuctionById(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Auction.Include("User").Where(x => x.Id == id).FirstOrDefault();
            }
        }
        public void UpdateWithClick(Auction auction, int UserId, int price)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                auction.Current_UserId = UserId;
                auction.Reserve_Price = price;
                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public bool UpdateTimer(int auctionId, TimeSpan timer, bool startStatus)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var auction = GetAuctionById(auctionId);
                auction.Auction_Time = timer;
                auction.StartStatus = startStatus;
                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() == 0 ? false : true;
            }
        }
        public bool UpdateIsclose(int auctionId)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var auction = GetAuctionById(auctionId);
                auction.IsClose = true;
                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() == 0 ? false : true;
            }
        }
    }
}