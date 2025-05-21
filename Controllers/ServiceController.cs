using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakthiAutomotive.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Inventory()
        {
            return View();
        }
        public ActionResult StockAdjust()
        {
            return View();
        }
    }
}