using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string incognito { get; set; }
        public int CountryDropdown { get; set; }

    }
}