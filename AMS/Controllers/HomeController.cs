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
        public ActionResult Index()
        {
          /*  WebSecurity.CreateUserAndAccount("Hari", "Harihara");
            Roles.CreateRole("Super Admin");
            Roles.CreateRole("User Admin");
            Roles.CreateRole("User");
            Roles.CreateRole("Customer");
            Roles.AddUserToRole("Hari", "User Admin"); */
            
            ViewBag.Message = "Welcome to Appointment management System!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

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
