/*
 * Author:
 *      Gan Zhexian Timothy
 * Version:
 *      2.2
 *  Summary:
 *      Business logic layer for User Admin
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
    public class UserAdminBLL
    {
        //make sure to use membership id, not user's userid
        public string GetUsername(int userID)
        {
            AMSEntities ae = new AMSEntities();
            UserProfile userProfile = ae.UserProfiles.Where(up => up.UserId == userID).FirstOrDefault();
            if (userProfile != null)
            {
                return userProfile.UserName;
            }
            return null;
        }

        //userId from User table
        public void DeleteUser(User user, AMSEntities ae)
        {
            //get username
            int membershipID = user.MembershipUserID;
            string username = GetUsername(membershipID);

            //Delete User
            ae.Users.DeleteObject(user);
            //Remove User Role
            string role = "";
            if (Roles.IsUserInRole(username, "User Admin"))
            {
                role = "User Admin";
                Roles.RemoveUserFromRole(username, role);
            }
            else if (Roles.IsUserInRole(username, "Customer"))
            {
                role = "Customer";
                Roles.RemoveUserFromRole(username, role);
            }
            if (Roles.IsUserInRole(username, "User"))
            {
                role = "User";
                Roles.RemoveUserFromRole(username, role);
            }
            //Delete Membership
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(username); //Membership.DeleteUser is not enough as Microsoft specifications are wrong; it does not delete from membership table
            Membership.DeleteUser(username);

        }

        public void CreateUserAdmin(string uName, string uEmail, string fName, string lName, string title, string description, string phone, int companyID)
        {
            if (!ValidateUserAdminFormat(uName, uEmail, fName, lName, title, description, phone, companyID))
            {
                throw new Exception("Incorrect Format");
            }
            if (!CompanyExists(companyID))
            {
                throw new Exception("Company does not exist");
            }
            if (UserExists(uName))
            {
                throw new Exception("User already exists");
            }
            string uPass = Membership.GeneratePassword(12, 1);
            AMSEntities ae = new AMSEntities();

            using (TransactionScope t = new TransactionScope())
            {
                /* Add to User .NET Membership */
                //UserProfile + webpages_membership tables
                WebSecurity.CreateUserAndAccount(uName, uPass);

                /* Add User .NET Membership to User Admin Role */
                Roles.AddUserToRole(uName, "User Admin");

                /* Add to Users table */
                User user = new User();
                user.MembershipUserID = WebSecurity.GetUserId(uName);
                user.UserFirstName = fName;
                user.UserLastName = lName;
                user.UserEmail = uEmail;
                user.UserTitle = title;
                user.UserDescription = description;
                user.UserPhone = phone;
                user.CompanyID = companyID;
                user.UserStatus = true;
                ae.Users.AddObject(user);

                ae.SaveChanges();
                t.Complete();
            }

            string subject = "Your Appointment Management System Account has been created";
            string content = "Dear " + title + " " + lName + ",";
            content += "\n\n" + "Your username is \"" + uName + "\" and your system generated password is \"" + uPass + "\".";
            content += "\n\n" + "Please change your password after logging in.";
            content += "\n\n" + "Regards,\n" + "The AMS Team";
            Emailer.Send(uEmail, subject, content);
        }

        private bool ValidateUserAdminFormat(string uName, string uEmail, string fName, string lName, string title, string description, string phone, int companyID)
        {
            if (uName == "" || uName == null){return false;}
            if (uEmail == "" || uEmail == null){return false;}
            if (fName == "" || fName == null) { return false; }
            if (lName == "" || lName == null) { return false; }
            if (title == "" || title == null) { return false; }
            if (description == "" || description == null) { return false; }
            if (phone == "" || uEmail == null) { return false; }
            if (companyID < 1) { return false; }
            return true;
        }

        private bool CompanyExists(int companyID)
        {
            AMSEntities ae = new AMSEntities();
            if (ae.Companies.Any(o => o.CompanyID == companyID))
            {
                return true;
            }
            return false;
        }

        private bool UserExists(string uName)
        {
            AMSEntities ae = new AMSEntities();
            if (ae.UserProfiles.Any(o => o.UserName == uName))
            {
                return true;
            }
            return false;
        }
    }
}