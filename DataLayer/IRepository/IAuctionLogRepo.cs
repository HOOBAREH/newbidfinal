using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
   public interface IAuctionLogRepo
    {
        List<AuctionLogs> GetAll();
        AuctionLogs GetByUserId(int id);
        AuctionLogs GetByAuctionId(int id);
        LogModel GetLast8ByAuctionId(int id);
        void Insert(AuctionLogs model);
    }
}
