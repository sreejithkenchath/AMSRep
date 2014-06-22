using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace AMS.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            /*WebSecurity.CreateUserAndAccount("superhari", "superhari");
            Roles.CreateRole("Super Admin");
            Roles.CreateRole("User Admin");
            Roles.CreateRole("User");
            Roles.CreateRole("Customer");
            Roles.AddUserToRole("superhari", "Super Admin"); */

            
            ViewBag.Message = "Welcome to Appointment management System!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
         //   int id =Convert.ToInt32( Membership.GetUser().ProviderUserKey.ToString());
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login() {
            return View();
        }

    }
}
