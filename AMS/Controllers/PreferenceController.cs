using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using AMS.Models;

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

        [Authorize(Roles = "User")]
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
                amsFacade.createUserPreference(userpreference);
                return RedirectToAction("DayTimePreference");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName", userpreference.UserID);
            return View(userpreference);
        }
        [Authorize(Roles = "User")]
        public ActionResult DayTimePreference(UserPreference userpreference)
        {
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName");
            return View(userpreference);
        }
        [HttpPost]
        public JsonResult GetContent(string day)
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
        [Authorize(Roles = "User")]
        public ActionResult CreateAppointmentSlots()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAppointmentSlots(AppointmentSlots slot)
        {
            if (ModelState.IsValid)
            {
                int userId = WebSecurity.CurrentUserId;
                //amsFacade.CreateSlots(slot,userId);
                ViewBag.Message = amsFacade.CreateSlots(slot,userId);
            }

           // ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName", userpreference.UserID);
            return View(slot);
        }

    }
}