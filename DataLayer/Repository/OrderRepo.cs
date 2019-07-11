using DataLayer.IRepository;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace DataLayer.Repository
{
    public class OrderRepo : IOrderRepo
    {
        public int GetPriceOfOrder(List<ShopCartItem> model)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                int totlaPrice = 0;
                foreach (var item in model)
                {
                    totlaPrice += (db.Product.Where(x => x.Id == item.ProductID).FirstOrDefault().Price) * item.Count;
                }
                return totlaPrice;
            }
        }
        public Order AddOrder(List<ShopCartItem> model, int userId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var order = new Order();
                using (QuiBidsEntities db = new QuiBidsEntities())
                {
                    List<OrderItem> orderItems = new List<OrderItem>();

                    order.DateTime = DateTime.Now;
                    order.IsFinaly = false;
                    order.TotalPrice = GetPriceOfOrder(model);
                    order.UserId = userId;

                    db.Order.Add(order);
                    db.SaveChanges();
                    foreach (var item in model)
                    {
                        var pr = db.Product.Where(x => x.Id == item.ProductID).FirstOrDefault().Price;
                        var orerItem = new OrderItem
                        {
                            OrderId = order.Id,
                            Price = pr,
                            ProductId = item.ProductID,
                            Quantity = item.Count
                        };

                        orderItems.Add(orerItem);
                    }
                    db.OrderItem.AddRange(orderItems);
                    db.SaveChanges();
                }
                scope.Complete();
                return order;
            }
        }
        public Order GetOrder(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Order.Find(id);
            }
        }
        public void UpdateIsFinaly(int id, string refId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (QuiBidsEntities db = new QuiBidsEntities())
                {
                    var order = db.Order.Find(id);
                    order.IsFinaly = true;
                    order.RefId = refId;
                    db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    var payment = new Payment
                    {
                        CreditCardNumber = "123",
                        OrderId = order.Id,
                        PayTime = DateTime.Now
                    };
                    db.Payment.Add(payment);
                    db.SaveChanges();
                }
                scope.Complete();

            }
        }

    }
}