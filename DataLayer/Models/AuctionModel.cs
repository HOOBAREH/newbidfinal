using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Models
{
    public class AuctionListModel
    {
        public AuctionModel Auctions { get; set; }
        public List<StatisticsModel> statisticsList { get; set; }

    }
    public class AuctionModel
    {
        public int AuctionId { get; set; }
        public List<string> ProductImage { get; set; }
        public string UserImage { get; set; }
        public int ProductId { get; set; }

        public TimeSpan Auction_Time { get; set; }

        public int Close_Time { get; set; }

        public int Reserve_Price { get; set; }

        public int BuyPrice { get; set; }

        public int CurrentBid_Id { get; set; }

        public int Current_UserId { get; set; }

        public bool StartStatus { get; set; }

        public bool IsClose { get; set; }

        public string colorStatus { get; set; }

        public User User { get; set; }
        
        public bool IsActive { get; set; }

    }
}