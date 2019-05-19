using DataLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class CountryRepo:ICountryRepo

    {
      
        public List<Countries> GetCountries()
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Countries.ToList();
            }
        }
        public Countries GetCountryById(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.Countries.Where(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}