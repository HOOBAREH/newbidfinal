using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
   public interface IOrderRepo
    {
        int GetPriceOfOrder(List<ShopCartItem> model);
        Order AddOrder(List<ShopCartItem> model, int userId);
        Order GetOrder(int id);
        void UpdateIsFinaly(int id, string refId);
    }
}
