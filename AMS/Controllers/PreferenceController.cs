using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace AMS.Controllers
{
    public class PreferenceController : Controller
    {
                //
        // GET: /Preference/Create
        private AMSBLLFacade.AMSBLLFacade amsFacade;
        AMSEntities db = new AMSEntities();
        public PreferenceController()
        {
            amsFacade = new AMSBLLFacade.AMSBLLFacade();
        }
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName");   
            return View();
        }

        //
        // POST: /Preference/Create

        [HttpPost]
        public ActionResult Create(UserPreference userpreference)
        {
            if (ModelState.IsValid)
            {
                amsFacade.createUserPreferencesAndAppointmentSlots(userpreference);
                return RedirectToAction("DayTimePreference");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName", userpreference.UserID);
            return View(userpreference);
        }

        public ActionResult DayTimePreference(UserPreference userpreference)
        {
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName");
            return View(userpreference);
        }
        [HttpPost]
        public JsonResult Test(string day)
        {
            List<TimePreference> tf = amsFacade.GetDayTimePreferences(WebSecurity.CurrentUserId,day);
            return Json(tf, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDayTimePreference(string day, string firstFrom, string firstTo, string secondFrom,
            string secondTo)
        {
            amsFacade.setDayTimePreference(WebSecurity.CurrentUserId, day, firstFrom, firstTo, secondFrom, secondTo);
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        
    }
}