/*
 * Author:
 *      Gan Zhexian Timothy
 * Version:
 *      2.0
 *  Summary:
 *      Code behind for creating a user admin
 */

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
using AMS_SuperAdmin.BLL;

namespace AMS_SuperAdmin.Forms
{
    public partial class CreateUserAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /** User clicks "Submit" button on aspx page */
        protected void submit_Click(object sender, EventArgs e)
        {
            /* get fields */
            string uName = this.uName.Text;
            string uEmail = this.uEmail.Text;
            string fName = this.fName.Text;
            string lName = this.lName.Text;
            string title = this.TitleList.SelectedValue;
            string description = this.description.Text;
            string phone = this.phone.Text;
            int companyID = Convert.ToInt32( Request.QueryString["companyID"] );

            /* create new user admin using BLL class */
            UserAdminBLL cbll = new UserAdminBLL();
            try
            {
                cbll.CreateUserAdmin(uName, uEmail, fName, lName, title, description, phone, companyID);
                //redirect user to see all companies
                Server.Transfer("ViewCompanies.aspx", true);
            }
            catch (Exception ex)
            {
                //set server validation text on aspx page if server side validation fails
                this.ServerValidation.Text = ex.Message;
            }
        }
    }
}