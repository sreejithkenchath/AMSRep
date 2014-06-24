using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Transactions;

namespace AMS_SuperAdmin.Forms
{
    public partial class CreateUserAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string uName = this.uName.Text;
            string uPass = Membership.GeneratePassword(12, 1);
            string uEmail = this.uEmail.Text;
            int companyID = Convert.ToInt32( Request.QueryString["companyID"] );

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
                user.UserFirstName = this.fName.Text;
                user.UserLastName = this.lName.Text;
                user.UserEmail = uEmail;
                user.UserTitle = this.title.Text;
                user.UserDescription = this.description.Text;
                user.UserPhone = this.phone.Text;
                user.CompanyID = companyID;
                ae.Users.AddObject(user);

                ae.SaveChanges();
                t.Complete();
            }

            string subject = "Your Appointment Management System Account has been created";
            string content = "Dear " + this.title.Text + " " + this.lName.Text + ",";
            content += "\n\n" + "Your username is \"" + uName + "\" and your system generated password is \"" + uPass + "\".";
            content += "\n\n" + "Please change your password after logging in.";
            content += "\n\n" + "Regards,\n" + "The AMS Team";
            Emailer.Send(uEmail, subject, content);

            /* Redirect to new page */
            Server.Transfer("ViewCompanies.aspx", true);
        }
    }
}