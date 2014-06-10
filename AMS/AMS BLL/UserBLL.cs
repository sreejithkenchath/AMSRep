using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace AMS.AMS_BLL
{
    public class UserBLL
    {
        public void AddNewUser(FormCollection collection)
        {
            AMSEntities ae=new AMSEntities();
            User uu=ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            User user=new AMS.User();
            String uname = collection.Get("UserName");
            String paswd=collection.Get("Password");
            String repaswd=collection.Get("RePassword");
            WebSecurity.CreateUserAndAccount(uname,paswd);
            Roles.AddUserToRole(uname,"User");
            user.MembershipUserID = WebSecurity.GetUserId(uname); ;
            user.UserFirstName=collection.Get("FirstName");
            user.UserLastName=collection.Get("LastName");
            user.UserTitle=collection.Get("Title");
            user.UserPhone=collection.Get("Phone");                        
            user.UserDescription=collection.Get("Desc");
            user.UserEmail = collection.Get("Email");
            user.CompanyID = uu.CompanyID;
            String status = collection.Get("Status");
            if (status == "Active")
                user.UserStatus = true;
            else
                user.UserStatus = true;
            ae.Users.AddObject(user);
            ae.SaveChanges();
        }
    }
}