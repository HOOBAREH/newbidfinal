using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace App_QuiBids.Models
{
    public class SessionContext
    {
        public void SetAuthenticationToken(string name, bool isPersistant, User userData)
        {
            string data = null;
            if (userData != null)
            {
                var r = JsonConvert.SerializeObject(userData);
                data = new JavaScriptSerializer().Serialize(r);
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddYears(1), isPersistant, userData.Id.ToString());

            string cookieData = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public User GetUserData()
        {
            User userData = null;

            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    userData = new JavaScriptSerializer().Deserialize(ticket.UserData, typeof(User)) as User;
                }
            }
            catch (Exception ex)
            {
            }

            return userData;
        }
    }
}