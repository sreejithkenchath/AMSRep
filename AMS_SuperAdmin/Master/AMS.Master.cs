/*
 * Author:
 *      Gan Zhexian Timothy
 * Version:
 *      1.2
 *  Summary:
 *      Code behind for master page, forcing login
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

namespace AMS_SuperAdmin.Master
{
    public partial class AMS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = (string)(Session["Username"]);
            if(username == null || ! Roles.IsUserInRole(username, "Super Admin"))
            {
                Response.Redirect("Login.aspx");
            }
            this.username.Text = "Welcome " + username;       
        }
    }
}