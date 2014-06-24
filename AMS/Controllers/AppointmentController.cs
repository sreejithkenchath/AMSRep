using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models;

namespace AMS.Controllers
{
    public class AppointmentController : Controller
    {
        //
        // GET: /Appointment/

        public ActionResult Index()
        {
           
            List<AppointmentList> a = ab.GetAppointmentDetails(WebSecurity.CurrentUserId);
 return View();

        }
        
        public ActionResult Preferences()
        {
            return View();
        }
    }
}
