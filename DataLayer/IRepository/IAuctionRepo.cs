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
        void UpdateWithClick(Auction auction, int id,int price);
        bool UpdateTimer(int auctionId, TimeSpan timer,bool startStatus);
        bool UpdateIsclose(int auctionId);
    }
}
