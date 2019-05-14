using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
    public interface ICountryRepo
    {
        List<Countries> GetCountries();
        Countries GetCountryById(int id);
    }
}
