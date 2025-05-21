using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakthiAutomotive.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Employees()
        {
            return View();
        }

        public ActionResult Transporter()
        {
            return View();
        }
        
        public ActionResult Shifts()
        {
            return View();

        }
        public ActionResult Products()
        {
            return View();

        }
        public ActionResult Customers()
        {
            return View();

        }
        public ActionResult Reason()
        {
            return View();

        }
        public ActionResult PageAction()
        {
            return View();

        }
    }
}