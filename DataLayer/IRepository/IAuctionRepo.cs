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
    }
}
