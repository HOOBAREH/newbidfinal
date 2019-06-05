using DataLayer.IRepository;
using DataLayer.Models;
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

                return db.Auction.Include("User").Include("Product").Where(x => x.Id == id).FirstOrDefault();
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
        public Auction UpdateIsclose(int auctionId)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var auction = GetAuctionById(auctionId);
                auction.IsClose = true;
                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return auction;
            }
        }
        public AuctionModel GetProductByAuction(int auctionId)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var query = (from au in db.Auction
                             join img in db.Image on au.ProductId equals img.ProductId into tbl
                             where au.Id == auctionId
                             join img2 in db.Image on  au.Current_UserId equals  img2.UserId  into tbl2
                             from y2 in tbl2.DefaultIfEmpty()
                            select new AuctionModel
                            {
                                ProductId = au.ProductId,
                                Auction_Time = au.Auction_Time,
                                Close_Time = au.Close_Time,
                                BuyPrice = au.BuyPrice,
                                IsClose = au.IsClose,
                                Reserve_Price = au.Reserve_Price != null ? au.Reserve_Price.Value : 0,
                                StartStatus = au.StartStatus,
                                ProductImage = tbl.Select(t=>t.ImageURL).ToList(),
                                User = au.User,
                                UserImage=tbl2.Select(x=>x.ImageURL).FirstOrDefault()
                            }).FirstOrDefault();
                return query;
            }
        }
    }
}