/*
 * Author:
 *      Gan Zhexian Timothy
 * Version:
 *      2.3
 *  Summary:
 *      Business logic layer for Company
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Transactions;

namespace AMS_SuperAdmin.BLL
{
    public class CompanyBLL
    {
        /** Delete a company based on its id */
        public void DeleteCompany(int companyID)
        {
            AMSEntities ae = new AMSEntities();
            using (TransactionScope t = new TransactionScope())
            {
                /* Delete users associated with company */
                List<User> users = ae.CreateObjectSet<User>().Where<User>(e => e.CompanyID == companyID).AsQueryable<User>().ToList<User>();
                UserAdminBLL uabll = new UserAdminBLL();
                foreach (User user in users)
                {
                    uabll.DeleteUser(user, ae);
                }

                /* Delete company */
                Company company = ae.Companies.Where(c => c.CompanyID == companyID).FirstOrDefault();
                ae.Companies.DeleteObject(company);

                /* Save changes and complete transaction */
                ae.SaveChanges();
                t.Complete();
            }
        }

        /** Create a company given parameters */
        public int CreateCompany(string name, string address, string phone)
        {
            /* Format validation */
            if( !ValidateCompanyFormat(name, address, phone) )
            {
                throw new Exception("Incorrect Format");
            }
            /* Business logic validation */
            if( CompanyExists(name) )
            {
                throw new Exception("Company already exists");
            }
            /* Create company object and save*/
            AMSEntities ae = new AMSEntities();
            Company company = new Company();
            company.CompanyName = name;
            company.CompanyAddress = address;
            company.CompanyPhone = phone;
            company.CompanyStatus = true;
            ae.Companies.AddObject(company);
            ae.SaveChanges();
            /* return company id */
            return company.CompanyID;
        }

        /* Format validation for company details */
        private bool ValidateCompanyFormat(string name, string address, string phone)
        {
            if (name == "" || name == null)
            {
                return false;
            }
            if (address == "" || address == null)
            {
                return false;
            }
            if (phone == "" || phone == null)
            {
                return false;
            }
            return true;
        }

        /** Business Logic validation to check if company exists */
        private bool CompanyExists(string cName)
        {
            AMSEntities ae = new AMSEntities();
            if (ae.Companies.Any(o => o.CompanyName == cName))
            {
                return true;
            }
            return false;
        }

        /** 
         * Return a new DataTable with Company Fields as columns, including a column for AddUserAdmin and DeleteCompany.
         * The returned DataTable is filled with the necessary rows here.
         */
        public DataTable FillTable()
        {
            DataTable table = new DataTable();

            /* Fill table with Company information */
            string connectionString = ConfigurationManager.ConnectionStrings["AMSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT CompanyID, CompanyName, CompanyAddress, CompanyPhone FROM Company ORDER BY CompanyID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(table);
                    }
                }
            }
            /* Adding URL column for Add User Admin to the table */
            table.Columns.Add("UserAdmins", typeof(String));
            table.Columns.Add("AddUserAdmin", typeof(String));
            table.Columns.Add("DeleteCompany", typeof(String));

            foreach (DataRow dr in table.Rows)
            {
                /* "User Admins" */
                //get a list of administrator names which are in company id=x
                int companyID = Convert.ToInt32(dr["CompanyID"]);
                AMSEntities ae = new AMSEntities(); ;
                /* Users: list of users in a particular company */
                List<User> users = ae.CreateObjectSet<User>().Where<User>(e => e.CompanyID == companyID).AsQueryable<User>().ToList<User>();

                string usernameList = "";
                foreach (User user in users)
                {
                    int userID = user.MembershipUserID;
                    /* UserProfiles: list of UserProfiles in a particular company */
                    List<UserProfile> userProfiles = ae.CreateObjectSet<UserProfile>().Where<UserProfile>(e => e.UserId == userID).AsQueryable<UserProfile>().ToList<UserProfile>();
                    foreach (UserProfile userProfile in userProfiles)
                    {
                        int userProfileID = userProfile.UserId;
                        string username = userProfile.UserName;
                        if (Roles.IsUserInRole(username, "User Admin"))
                        {
                            usernameList += username + " ";
                        }
                    }

                }
                dr["UserAdmins"] = usernameList;
                /* "Add User Admins" */
                dr["AddUserAdmin"] = companyID;
                dr["DeleteCompany"] = companyID;
            }
            return table;
        }
    }
}