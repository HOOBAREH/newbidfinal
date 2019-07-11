using DataLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class ProductRepo:IProductRepo
    {
        public List<Product> GetAllProduct()
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Product.ToList();
            }
        }
        public Product GetProduct(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Product.Where(x=>x.Id==id).FirstOrDefault();
            }
        }

    }
}