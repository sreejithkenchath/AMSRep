using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;
using System.Transactions;
using AMS.Repositories;
using AMS.Models;


namespace AMS.AMS_BLL
{
    public class CustomerBLL
    {
        private string Message;
        //private bool IsValid;
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        public CustomerBLL()
        {
            //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
            this.DataStore = new EFRepository();
        }

        public string AddCustomer(FormCollection collection, Customer c)
        {
            Message = null;
            //IsValid=true;
            AMSEntities ae = new AMSEntities();
            string uname = collection.Get("UserName");
            string pwd = collection.Get("Password");
            string repwd = collection.Get("ReTypePassword");


            //var query= DataStore.Get<UserProfile>(b => b.UserName == uname);
            if (WebSecurity.UserExists(uname))
            {
                SetError("User Already exsist");
            }


            if (pwd == repwd)
            {

                using (TransactionScope ts = new TransactionScope())
                {

                    WebSecurity.CreateUserAndAccount(uname, pwd);
                    Roles.AddUserToRole(uname, "Customer");
                    c.MembershipUserID = WebSecurity.GetUserId(uname);
                    c.CustStatus = true;

                    DataStore.Create(c);
                    DataStore.SaveChanges();
                    Emailer.Send(c.CustEmail, "Registration confirmed", "Welcome to AMS");
                    ts.Complete();
                };
                Message = "Successfully Registered";

                return Message;
            }
            else
            {

                return Message;
            }


        }



        public void SetError(string message)
        {

            Message += message;
        }
    }
}

        
    
