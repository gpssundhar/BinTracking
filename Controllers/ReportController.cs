using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakthiAutomotive.Models;

namespace SakthiAutomotive.Controllers
{
    public class ReportController : Controller
    {
        HomeController ObjCom = new HomeController();
        readonly MasterLogic objMas = new MasterLogic();
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