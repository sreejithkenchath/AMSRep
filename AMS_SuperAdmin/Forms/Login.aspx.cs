/*
 * Author:
 *      Gan Zhexian Timothy
 * Version:
 *      1.2
 *  Summary:
 *      Login page for web forms
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace AMS_SuperAdmin.Forms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebSecurity.Logout();
            Session.Remove("Username"); 
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (WebSecurity.Login(txtUserName.Text, txtPassword.Text, persistCookie: true))
            {
                Session["Username"] = txtUserName.Text;
                Response.Redirect("ViewCompanies.aspx");
            }
            else {
                Response.Redirect("Login.aspx");
            }
        }
    }
}