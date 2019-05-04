using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_QuiBids.Models
{
    public class RegisterModel
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}