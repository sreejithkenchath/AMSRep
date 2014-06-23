using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using AMS.Repositories;
using WebMatrix.WebData;
using System.Web.Security;
using System.Transactions;

namespace AMS.AMS_BLL
{
    public class UserBLL
    {
        private string Message;
        private bool IsValid;
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        public UserBLL()
        {
            //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
            this.DataStore = new EFRepository();
        }

        public string AddNewUser(FormCollection collection,User user)
        {
            Message = null;
            IsValid=true;
            AMSEntities ae=new AMSEntities();
           // User emailcheck = ae.Users.Where(e => e.UserEmail == user.UserEmail).Single();
            
         //   if (emailcheck != null)
         //       SetError("Email Id already Exists");

            //User uu=ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            User uu = DataStore.Get<User>(e => e.MembershipUserID == WebSecurity.CurrentUserId);
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
                    //ae.Users.AddObject(user);
                    //ae.SaveChanges();
                    DataStore.Create(user);
                    DataStore.SaveChanges();
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

        public List<User> GetUsersForCompany(int p)
        {
            return DataStore.Filter<User>(e => e.CompanyID == p).ToList();
        }

        public User GetUserbyId(int p)
        {
           return DataStore.Get<User>(e => e.MembershipUserID == p);
        }

        internal List<User> getUsers()
        {
            return DataStore.All<User>().ToList();
        }
    }
}