using DataLayer.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Repository
{
    public class UserRepo : IUserRepo
    {
        private QuiBidsEntities _dbContext = new QuiBidsEntities();

        public UserRepo()
        {

        }

        public User GetUserById(int id)
        {
            return _dbContext.User.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _dbContext.User.ToList();
        }

        public User Login(string userName, string password)
        {
            var user = _dbContext.User.Where(x => x.Email == userName && x.Password == password).FirstOrDefault();
            return user;
        }
        public bool UsernameExists(string userName)
        {
            if (_dbContext.User.Where(x => x.Email == userName).Any())
                return true;
            return false;
        }
        public bool Register(User user)
        {
            _dbContext.User.Add(user);
            return _dbContext.SaveChanges() == 1 ? true : false;

        }
        /// <summary>
        /// کم کردن بید های کاربر 
        /// اگر "ریل بید" مقدار داشت از آن کم میکند.
        /// اگر "ریل بید" مقدار نداشت از "وچر بید" کم میکند.
        /// اگر در مزایده برنده شد "وچر بید" تایید شده اند.
        /// اگر در مزایده برنده نشد از "وچر بید" نمی تواند برای خرید استفاده کند.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User LowerBids(int userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                if (user.RealBid != 0)
                    user.RealBid--;

                if (user.VoucherBid != 0)
                    user.VoucherBid--;

                _dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return user;
            }
            return null;

        }
        public void AddToBids(int userId)
        {
            var user = GetUserById(userId);
            user.RealBid++;
            _dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void UpdateProfile(UserModel model)
        {
            var user = GetUserById(model.Id);
            if (user != null)
            {
                //user.Email = model.Email;
                user.HideLocation = (model.incognito == "Yes" ? true : false);
                if (model.CountryDropdown != 0)
                {
                    user.CountryId = model.CountryDropdown;
                }
                _dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }
        public bool ChangePass(string pass,int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.Password = pass;
                _dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                return _dbContext.SaveChanges() == 1 ? true : false;
            }
            return false;
        }
    }
}