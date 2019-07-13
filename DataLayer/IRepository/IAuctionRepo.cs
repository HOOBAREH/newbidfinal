using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
    public interface IAuctionRepo
    {
        List<Auction> GetAuctions();
        Auction GetAuctionById(int id);
        int ParticipateInAuctions(int auctionId, int UserId);
        bool UpdateTimer(int auctionId, TimeSpan timer, bool startStatus, bool? isclose);
        Auction UpdateTimer2(int auctionId, TimeSpan timer, bool startStatus, bool? isclose);
        Auction UpdateIsclose(int auctionId);
        AuctionModel GetProductByAuction(int auctionId);
        List<StatisticsModel> GetStatistics();
        bool CheckStatus(int id);
    }
}
