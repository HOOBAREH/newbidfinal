﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
    public interface IUserRepo
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User Login(string userName, string password);
        bool Register(User user);
        bool UsernameExists(string userName);
        int LowerBids(int userId);
    }
}
