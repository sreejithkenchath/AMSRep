using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMS_SuperAdmin.BLL;

namespace AMS_SuperAdmin.Forms
{
    public partial class DeleteCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int companyID = Convert.ToInt32(Request.QueryString["companyID"]);
            CompanyBLL cbll = new CompanyBLL();
            cbll.DeleteCompany(companyID);
            Server.Transfer("ViewCompanies.aspx", true);
        }
    }
}