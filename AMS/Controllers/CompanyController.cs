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
                CompanyBLL cb = new CompanyBLL();
                cb.EditCompany(company);
                //.Companies.Attach(company);
               // db.ObjectStateManager.ChangeObjectState(company, EntityState.Modified);
                // TODO: BLL code
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

         // GET: /Company/Delete/5

        

        //
        // POST: /Company/Delete/5

        
      
    }
}