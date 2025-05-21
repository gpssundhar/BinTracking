using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakthiAutomotive.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult RptStockAdjust()
        {
            return View();
        }
        public ActionResult RptInventory()
        {
            return View();
        }
        public ActionResult RptCustomerInventory()
        {
            return View();
        }
    }
}