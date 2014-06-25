/*
 * Author:
 *      Gan Zhexian Timothy
 * Version:
 *      1.5
 *  Summary:
 *     Code behind for viewing companies
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using AMS_SuperAdmin.BLL;

namespace AMS_SuperAdmin.Forms
{
    public partial class ViewCompanies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGridView();
            }
        }

        private void FillGridView()
        {
            CompanyBLL cbll = new CompanyBLL();
            DataTable table = cbll.FillTable();

            GridViewCompanies.DataSource = table;
            GridViewCompanies.DataBind();
        }

        /* Override this function in order for GridView rendering to work on page load */
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected string FindCompanyNumber()
        {
            return "hello";
        }

    }
}