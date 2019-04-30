using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DataLayer.Utilites
{
    public class Helpers
        {

            public string MiladiToShamsi(DateTime dt)
            {
                PersianCalendar pc = new PersianCalendar();
                string str = pc.GetYear(dt).ToString("####/") + pc.GetMonth(dt).ToString("##/") + pc.GetDayOfMonth(dt).ToString();
                return str;

            }
            public string Encryption(string password)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] encrypt;
                UTF8Encoding encode = new UTF8Encoding();
                //encrypt the given password string into Encrypted data  
                encrypt = md5.ComputeHash(encode.GetBytes(password));
                StringBuilder encryptdata = new StringBuilder();
                //Create a new string by using the encrypted data  
                for (int i = 0; i < encrypt.Length; i++)
                {
                    encryptdata.Append(encrypt[i].ToString());
                }
                return encryptdata.ToString();
            }

        }
    }
