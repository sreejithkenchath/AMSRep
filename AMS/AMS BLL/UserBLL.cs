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
        private string Message;
        private bool IsValid;
        public string AddNewUser(FormCollection collection,User user)
        {
            Message = null;
            IsValid=true;
            AMSEntities ae=new AMSEntities();
           // User emailcheck = ae.Users.Where(e => e.UserEmail == user.UserEmail).Single();
            
         //   if (emailcheck != null)
         //       SetError("Email Id already Exists");

              User uu=ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            
            String uname = collection.Get("UserName");
            String paswd=collection.Get("Password");
            user.CompanyID = uu.CompanyID;
            if (IsValid)
            {
                using (TransactionScope t = new TransactionScope())
                {
                    WebSecurity.CreateUserAndAccount(uname, paswd);
                    Roles.AddUserToRole(uname, "User");
                    user.MembershipUserID = WebSecurity.GetUserId(uname);
                    ae.Users.AddObject(user);
                    ae.SaveChanges();
                    Emailer.Send(user.UserEmail, "Welcome to AMS", "Please reset your password");
                    t.Complete();
                };
                Message="successfully added a new user";
                return Message;
            }
            else
            {
                return Message;
            }
        }

        public void SetError(string message)
        {
            IsValid = false;
            Message += message;
        }
    }
}