using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.AMS_BLL;
using WebMatrix.WebData;
using AMS.Models;


namespace AMS.Controllers
{
    public class AppointmentController : Controller
    {
        private AMSEntities db = new AMSEntities();
        //
        // GET: /Appointment/
        //
        // GET: /Appointment/
        AMSEntities objEntities = new AMSEntities();

        AppointmentBLL AP = new AppointmentBLL();
        [Authorize(Roles = "Customer")]
        public ActionResult Appointment()
        {
            return View();

        }
        public ActionResult Index()
        {
            AppointmentBLL ab = new AppointmentBLL();
            List<AppointmentList> a = ab.GetAppointmentDetails(WebSecurity.CurrentUserId);
            return View(a);
        }

       
  
        public JsonResult GetAppointmentData(int UserID, String Date)
        {
            //DateTime d=DateTime.Now;
            DateTime da = Convert.ToDateTime(Date);
            List<DynamicAppointment> DAppointments = AP.GetAppointments(UserID, da);
            return Json(DAppointments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompany()
        {
            var Companies = AP.GetCompanies();

            return Json(Companies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsers(int CompanyID)
        {
            if (CompanyID == 0)
            {
                return null;
            }
            else
            {
                var Users = AP.GetUsers(CompanyID);
                return Json(Users, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public String MakeAppointment(int UserID, String TimeFrom, String Timeto, String Date)
        {
            string message = string.Empty;
            try
            {
                DateTime D = Convert.ToDateTime(Date);
                int membershipId = WebSecurity.GetUserId(User.Identity.Name);
                message = AP.MakeApppointment(membershipId, D, UserID, TimeFrom, Timeto);
                return message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return message;
            }


        }


    }
}
