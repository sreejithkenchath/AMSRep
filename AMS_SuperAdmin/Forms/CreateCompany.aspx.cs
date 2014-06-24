using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace AMS_SuperAdmin.Forms
{
    public partial class CreateCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            AMSEntities ae = new AMSEntities();
            Company company = new Company();
            company.CompanyName = this.cName.Text;
            company.CompanyAddress = this.cAddress.Text;
            company.CompanyPhone = this.cPhone.Text;
            company.CompanyStatus = true;
            ae.Companies.AddObject(company);
            ae.SaveChanges();

            string url = "CreateUserAdmin.aspx" + "?companyID=" + company.CompanyID;
            Server.Transfer(url, true);
        }
    }
}