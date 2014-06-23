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
         public ActionResult Index()
        {
            message = "";
            AMSEntities ae = new AMSEntities();
            User uu = ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            List<User> users = ae.Users.Where(e => e.CompanyID==uu.CompanyID).ToList();
            //ViewBag["Message"] = message;
            return View(users);
        }

         public ActionResult ExportUsersList()
         {
             AMSEntities ae = new AMSEntities();
             User uu = ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
             List<User> users = ae.Users.Where(e => e.CompanyID == uu.CompanyID).ToList();

             var d =(from s in users
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

        public ActionResult AddUser()
        {
                     return View("AddNewUser");
        }
        [HttpPost]
        public void AddUser(FormCollection collection, User user)
        {
            UserBLL ub = new UserBLL();
             String message=ub.AddNewUser(collection,user);
             
             Index();
        }
    }
}
