using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Repositories;

namespace AMS.AMS_BLL
{
    public class CompanyBLL
    {
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        public CompanyBLL()
        {
            //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
            this.DataStore = new EFRepository();
        }
        
        public Company GetCompanyDetails(int UserID)
        {
            User user = DataStore.Get<User>(e => e.MembershipUserID == UserID);
            Company company = DataStore.Get<Company>(e => e.CompanyID == user.CompanyID);
            return company;
        }

        //int GetCompany(int uid)
        //{
        //  User u = ae.Users.Where(e => e.MembershipUserID == uid).SingleOrDefault();
        //  return u.CompanyID;
        //}

        public void EditCompany(Company company)
        {
            DataStore.Update<Company>(company);
        }
    }
}