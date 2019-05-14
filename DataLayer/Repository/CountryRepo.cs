using DataLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class CountryRepo:ICountryRepo

    {
        private QuiBidsEntities _dbContext = new QuiBidsEntities();
        public List<Countries> GetCountries()
        {
            return _dbContext.Countries.ToList();
        }
        public Countries GetCountryById(int id)
        {
            return _dbContext.Countries.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}