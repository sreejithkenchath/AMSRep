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
            table.Columns.Add("UserAdmins"  , typeof(String));
            table.Columns.Add("AddUserAdmin", typeof(String));

            foreach (DataRow dr in table.Rows)
            {
                /* "User Admins" */
                //get a list of administrator names which are in company id=x
                int companyID = Convert.ToInt32( dr["CompanyID"] );
                AMSEntities ae = new AMSEntities();;
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
                        if(Roles.IsUserInRole(username, "User Admin")){
                            usernameList += username + " ";
                        }
                    }

                }
                dr["UserAdmins"] = usernameList;
                /* "Add User Admins" */
                dr["AddUserAdmin"] = companyID;
            }

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