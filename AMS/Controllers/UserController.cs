using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.AMS_BLL;
using WebMatrix.WebData;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace AMS.Controllers
{
    public class UserController : Controller
    {
        String message;
        protected UserBLL userBll;
        public UserController()
        {
            userBll = new UserBLL();
        }

        public ActionResult Index()
        {
            message = "";
            AMSEntities ae = new AMSEntities();
            User user = userBll.GetUserbyId(WebSecurity.CurrentUserId);
            //User uu = ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            //List<User> users = ae.Users.Where(e => e.CompanyID==uu.CompanyID).ToList();
            List<User> users = userBll.GetUsersForCompany(user.CompanyID);
            //ViewBag["Message"] = message;
            return View(users);
        }

        public ActionResult ExportUsersList()
        {
            //AMSEntities ae = new AMSEntities();
            //User uu = ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            //List<User> users = ae.Users.Where(e => e.CompanyID == uu.CompanyID).ToList();
            User user = userBll.GetUserbyId(WebSecurity.CurrentUserId);
            List<User> users = userBll.GetUsersForCompany(user.CompanyID);
            var d = (from s in users
                     select new
                     {
                         UserTitle = s.UserTitle ?? null,
                         UserFirstName = s.UserFirstName ?? null,
                         UserLastName = s.UserLastName ?? null,
                         UserEmail = s.UserEmail ?? null,
                         UserPhone = s.UserPhone ?? null,
                     }).ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "MyUsers.rpt"));
            rd.SetDataSource(d);


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();



            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "MyusersList.pdf");
            }

            catch (Exception e)
            {
                throw;
            }


        }
        public ActionResult MyProfile()
        {
            UserBLL ub = new UserBLL();
            User u = ub.GetUserDetails(WebSecurity.CurrentUserId);
            return View(u);
        }



        public ActionResult AddUser()
        {
            return View("AddNewUser");
        }
        [HttpPost]
        public void AddUser(FormCollection collection, User user)
        {
            UserBLL ub = new UserBLL();
            String message = ub.AddNewUser(collection, user);

            Index();
        }
    }
}
