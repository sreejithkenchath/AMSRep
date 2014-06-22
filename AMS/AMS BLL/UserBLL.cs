using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;
using System.Transactions;

namespace AMS.AMS_BLL
{
    public class UserBLL
    {
        public void AddNewUser(FormCollection collection,User user)
        {
            AMSEntities ae=new AMSEntities();
            User uu=ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            
            String uname = collection.Get("UserName");
            String paswd=collection.Get("Password");

            user.CompanyID = uu.CompanyID;
            
            using (TransactionScope t = new TransactionScope())
            {
                WebSecurity.CreateUserAndAccount(uname, paswd);
                Roles.AddUserToRole(uname, "User");
                user.MembershipUserID = WebSecurity.GetUserId(uname); ;
                ae.Users.AddObject(user);
                ae.SaveChanges();
                t.Complete();
            };
            
        }
    }
}