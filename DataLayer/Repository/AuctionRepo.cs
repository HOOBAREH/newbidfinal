﻿using DataLayer.IRepository;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
                var result = db.Auction.Include("Product").Include("User").Where(x => x.IsActive && !x.IsClose).OrderBy(x => x.Auction_Time).Take(20).ToList();
                return result;
            }
        }
        public Auction GetAuctionById(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {

                return db.Auction.Include("User").Include("Product").Where(x => x.Id == id).FirstOrDefault();
            }
        }
        public int ParticipateInAuctions(int auctionId, int UserId)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                using (QuiBidsEntities db = new QuiBidsEntities())
                {
                    #region AddToPrice_Auction
                    var auction = db.Auction.Where(x => x.Id == auctionId).FirstOrDefault();
                    auction.Current_UserId = UserId;
                    auction.Reserve_Price++;//moshakas shavad chand vahed add shavad
                    auction.Auction_Time = TimeSpan.FromSeconds(auction.Close_Time);
                    db.Entry(auction).State = System.Data.Entity.EntityState.Modified;

                    var s = db.SaveChanges();
                    #endregion

                    #region LowerBidOfUser
                    var user = db.User.Where(x => x.Id == UserId).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.RealBid != 0)
                            user.RealBid--;
                        //if (user.VoucherBid != 0)
                        //    user.VoucherBid--;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    #endregion

                    #region AddLog
                    AuctionLogs log = new AuctionLogs
                    {
                        AuctionId = auctionId,
                        DateTime = DateTime.Now,
                        TypeBid = 1,//EditType
                        UserId = UserId,
                        Price = auction.Reserve_Price
                    };
                    db.AuctionLogs.Add(log);
                    db.SaveChanges();
                    #endregion
                    result = auction.Reserve_Price.Value;
                }
                scope.Complete();
            }
            return result;
        }
        public bool UpdateTimer(int auctionId, TimeSpan timer, bool startStatus, bool? isclose)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var auction = GetAuctionById(auctionId);
                auction.Auction_Time = timer;
                if (isclose != null)
                {
                    auction.IsClose = isclose.Value;
                }
                auction.StartStatus = startStatus;
                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() == 0 ? false : true;
            }
        }
        public Auction UpdateTimer2(int auctionId, TimeSpan timer, bool startStatus, bool? isclose)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var auction = GetAuctionById(auctionId);
                //Int64 tooBigBits = timer.Ticks;
                //Int64 truncated = tooBigBits >> 24;
                //TimeSpan temp = TimeSpan.FromTicks(truncated);
                auction.Auction_Time = timer;
                if (isclose != null)
                {
                    auction.IsClose = isclose.Value;
                }
                auction.StartStatus = startStatus;

                var result = db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                var s = db.SaveChanges();
                return auction;
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
                             join img2 in db.Image on au.Current_UserId equals img2.UserId into tbl2
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
                                 ProductImage = tbl.Select(t => t.ImageURL).ToList(),
                                 User = au.User,
                                 UserImage = tbl2.Select(x => x.ImageURL).FirstOrDefault(),
                                 AuctionId = au.Id
                             }).FirstOrDefault();
                return query;
            }
        }
        public List<StatisticsModel> GetStatistics()
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var result = (from au in db.Auction
                                  //join img in db.Image on au.ProductId equals img.ProductId into img

                              join Al in db.AuctionLogs on au.Id equals Al.AuctionId into log
                              where au.IsActive && au.IsClose
                              select new StatisticsModel
                              {
                                  AuctionId = au.Id,
                                  BidderName = au.User.Fname,
                                  BidsSpent = log.Count(),
                                  EndPrice = au.Reserve_Price.Value,
                                  TotalPaid = au.Reserve_Price.Value,
                                  Img = au.User.Image
                              }).ToList();
                return result;
            }
        }
        public bool CheckStatus(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Auction.Find(id).StartStatus;
            }
        }
    }
}