using Models;
using System;
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
        bool UsernameExists(string userName, string number);
        User LowerBids(int userId);
        void UpdateProfile(UserModel model);
        bool ChangePass(string pass, int id);
        void LastLogin(int id);
        User UpdateImage(int id, string name);
        bool AddToBids(int userId);
        int GetBidsById(int id);
    }
}
