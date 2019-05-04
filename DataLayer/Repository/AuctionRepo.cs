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
    }
}