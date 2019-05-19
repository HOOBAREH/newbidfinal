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
     

        public UserRepo()
        {

        }

        public User GetUserById(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.User.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public List<User> GetUsers()
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                return db.User.ToList();
            }
        }

        public User Login(string userName, string password)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var user = db.User.Where(x => x.Email == userName && x.Password == password).FirstOrDefault();
                return user;
            }
        }
        public bool UsernameExists(string userName)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                if (db.User.Where(x => x.Email == userName).Any())
                    return true;
                return false;
            }
        }
        public bool Register(User user)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                db.User.Add(user);
                return db.SaveChanges() == 1 ? true : false;
            }

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
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var user = GetUserById(userId);
                if (user != null)
                {
                    if (user.RealBid != 0)
                        user.RealBid--;

                    if (user.VoucherBid != 0)
                        user.VoucherBid--;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return user;
                }
                return null;
            }
        }
        public void AddToBids(int userId)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var user = GetUserById(userId);
                user.RealBid++;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void UpdateProfile(UserModel model)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
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
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public bool ChangePass(string pass,int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var user = GetUserById(id);
                if (user != null)
                {
                    user.Password = pass;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    return db.SaveChanges() == 1 ? true : false;
                }
                return false;
            }
        }
        public void LastLogin(int id)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var user = GetUserById(id);
                if (user != null)
                {
                    user.LastLogin = DateTime.Now;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public User UpdateImage(int id,string name)
        {
            using (QuiBidsEntities db = new QuiBidsEntities())
            {
                var user = GetUserById(id);
                if (user != null)
                {
                    user.Image = name;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return user;
            }
        }
    }
}