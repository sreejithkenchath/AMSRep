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

        public ActionResult Index()
        {
            AppointmentBLL ab = new AppointmentBLL();
            List<AppointmentList> a = ab.GetAppointmentDetails(WebSecurity.CurrentUserId);
            return View(a);
        }




    }
}
