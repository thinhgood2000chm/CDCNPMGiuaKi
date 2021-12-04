using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    public class LoginRegis
    {
        CenterManagerEntities db = new CenterManagerEntities();
        public bool Register(userLogin u)
        {
            try
            {
                db.userLogins.Add(u);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
         public userLogin GetAccountByUsername(string username)
         {
             return db.userLogins.FirstOrDefault(i => i.username == username);
         }
        public bool CheckLogin(string username, string password)
        {
            var acc = db.userLogins.FirstOrDefault(u => u.username == username && u.password == password);
            if (acc != null)
            {
                return true;
            }
            return false;

        }
    }
}