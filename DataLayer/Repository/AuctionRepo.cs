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
        private QuiBidsEntities _dbContext = new QuiBidsEntities();
        public List<Auction> GetAuctions()
        {
            return _dbContext.Auction.Include("Product").ToList();
        }
        public Auction GetAuctionById(int id)
        {
            return _dbContext.Auction.Where(x => x.Id == id).FirstOrDefault();
        }
        public void UpdateWithClick(Auction auction, int UserId, int price)
        {
            auction.Current_UserId = UserId;
            auction.Reserve_Price = price;
            _dbContext.Entry(auction).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public bool UpdateTimer(int auctionId, TimeSpan timer)
        {
            var auction = GetAuctionById(auctionId);
            auction.Auction_Time = timer;
            _dbContext.Entry(auction).State = System.Data.Entity.EntityState.Modified;
            return _dbContext.SaveChanges() == 0 ? false : true;
        }
    }
}