using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BinTracking.Controllers
{
    public class DashboardController : Controller
    {
       
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}