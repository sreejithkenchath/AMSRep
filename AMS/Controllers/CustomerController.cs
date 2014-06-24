﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.AMS_BLL;
using WebMatrix.WebData;
using System.IO;

namespace AMS.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        [AllowAnonymous]
        public void  Index()
        {
            Response.Redirect("Index.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(FormCollection collection, Customer c)
        {
           
                CustomerBLL cb = new CustomerBLL();
                string message = cb.AddCustomer(collection, c);
                if (message == null)
                {
                    ViewBag.Message = "Password Mismatch";
                    return View(c);
                }
                else
                {

                    //ViewData["Result"] = message;
                    //return View("Result");
                    return RedirectToAction("Login", "Account");
                }
          
            
        }
       
            
        }

    }

