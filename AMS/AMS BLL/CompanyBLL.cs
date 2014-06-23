using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.AMS_BLL
{
    public class CompanyBLL
    {
        AMSEntities ae = new AMSEntities();
        
        public Company GetCompanyDetails(int UserID)
        {
                int CompID = GetCompanyID(UserID);
                Company c = ae.Companies.Where(e => e.CompanyID == CompID).SingleOrDefault();
                return c;
        }

        int GetCompanyID(int uid)
        {
            User u = ae.Users.Where(e => e.MembershipUserID == uid).SingleOrDefault();
            return u.CompanyID;
        }

        public void EditCompany()
        {

        }
    }
}