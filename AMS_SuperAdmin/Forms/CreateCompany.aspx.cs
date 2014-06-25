using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;
using AMS_SuperAdmin.BLL;

namespace AMS_SuperAdmin.Forms
{
    public partial class CreateCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /** User clicks "Submit" button on aspx page */
        protected void submit_Click(object sender, EventArgs e)
        {
            CompanyBLL cbll = new CompanyBLL();
            try
            {
                //reset server validation text on aspx page
                this.ServerValidation.Text = "";
                //create new company using BLL class
                int companyID = cbll.CreateCompany(this.cName.Text, this.cAddress.Text, this.cPhone.Text);
                //redirect user to create a new user admin for the created company
                string url = "CreateUserAdmin.aspx" + "?companyID=" + companyID;
                Server.Transfer(url, true);
            }
            catch (Exception ex)
            {
                //set server validation text on aspx page if server side validation fails
                this.ServerValidation.Text = ex.Message;
            }
        }
    }
}