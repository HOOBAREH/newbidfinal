using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Model
{
    public class AuctionListModel
    {
        List<AuctionModel> Auctions = new List<AuctionModel>();
    }
    public class AuctionModel
    {
        public int ProductId { get; set; }
        public int id { get; set; }
        public TimeSpan Auction_Time { get; set; }
        public int Close_Time { get; set; }
        public int Reserve_Price { get; set; }
        public int EndPrice { get; set; }
        public int CurrentBid_Id { get; set; }
        public int Current_UserId { get; set; }
        public int Current_UserName { get; set; }
    }
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}