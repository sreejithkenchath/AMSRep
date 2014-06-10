using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.AMS_BLL;
using WebMatrix.WebData;

namespace AMS.Controllers
{
    public class UserController : Controller
    {
         public ActionResult Index()
        {
            AMSEntities ae = new AMSEntities();
            User uu = ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            List<User> users = ae.Users.Where(e => e.CompanyID==uu.CompanyID).ToList();
            return View(users);
        }

        public ActionResult AddUser()
        {
                     return View();
        }
        [HttpPost]
        public void AddUser(FormCollection collection)
        {
            UserBLL ub = new UserBLL();
             ub.AddNewUser(collection);
             Index();
        }
    }
}
