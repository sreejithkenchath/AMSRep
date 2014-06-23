using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.AMS_BLL;
using WebMatrix.WebData;

namespace AMS.Controllers
{
    public class CompanyController : Controller
    {
        private AMSEntities db = new AMSEntities();

        
        public ActionResult Index()
        {
            CompanyBLL cb = new CompanyBLL();
            Company c=cb.GetCompanyDetails(WebSecurity.CurrentUserId);
            return View(c);
        }
                
        public ActionResult Edit(int id = 0)
        {
            CompanyBLL cb = new CompanyBLL();
            Company c = cb.GetCompanyDetails(WebSecurity.CurrentUserId);
             return View(c);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Attach(company);
               // db.ObjectStateManager.ChangeObjectState(company, EntityState.Modified);
                // TODO: BLL code
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

         // GET: /Company/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Company company = db.Companies.Single(c => c.CompanyID == id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Single(c => c.CompanyID == id);
            //db.Companies.DeleteObject(company);
            //TODO: BLL CODE
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}