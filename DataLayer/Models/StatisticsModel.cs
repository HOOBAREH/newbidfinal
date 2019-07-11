using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Models
{

    public class StatisticsModel
    {
        public string Img { get; set; }
        public int AuctionId { get; set; }
        public int EndPrice { get; set; }
        public string BidderName { get; set; }
        public int BidsSpent { get; set; }
        public int TotalPaid { get; set; }
        public int Savings { get; set; }
    }   
}