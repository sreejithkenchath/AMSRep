using System;
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

        public ActionResult Index()
        {   
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(FormCollection collection, Customer c)
        {
            CustomerBLL cb = new CustomerBLL();
            string message = cb.AddCustomer(collection, c);
            return View(message);
            
        }
            
        }

    }

