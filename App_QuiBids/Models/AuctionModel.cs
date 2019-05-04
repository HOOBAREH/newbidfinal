﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_QuiBids.Models
{
    public class AuctionListModel
    {
        List<AuctionModel> Auctions = new List<AuctionModel>();
    }
    public class AuctionModel
    {
        public int id { get; set; }
        public int AuctionId { get; set; }
        public int Open_Time { get; set; }
        public int Close_Time { get; set; }
        public int? Reserve_Price { get; set; }
        public int? EndPrice { get; set; }
        public int? CurrentBid_Id { get; set; }
        public int? Current_UserId { get; set; }
    }
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}