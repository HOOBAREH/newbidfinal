using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_QuiBids.Models
{
    
    public class AuctionModel
    {
        public int ProductId { get; set; }
        public int id { get; set; }
        public TimeSpan Auction_Time { get; set; }
        public int Close_Time { get; set; }
        public int? Reserve_Price { get; set; }
        public int EndPrice { get; set; }
        public int? CurrentBid_Id { get; set; }
        public int? Current_UserId { get; set; }
        public string colorStatus { get; set; }
        public User User { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public Product Product { get; set; }

    }
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
    public class IndexAuction
    {
        public int Id { get; set; }
        public string Auction_Time { get; set; }
        public string Color { get; set; }
        public bool Status { get; set; }


    }
}