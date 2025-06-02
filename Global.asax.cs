using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SakthiAutomotive
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            int ret = 0;
            string LogPath, RptPath, ImagePath;
            DateTime dtFlTm;

            LogPath = RptPath = ImagePath = "";

            RptPath = Server.MapPath("") + "\\" + Globals.RPTPATH + "\\";
            if (Directory.Exists(RptPath) == false)
                Directory.CreateDirectory(RptPath);

            ImagePath = Server.MapPath("") + "\\" + Globals.IMGPATH + "\\";
            if (Directory.Exists(ImagePath) == false)
                Directory.CreateDirectory(ImagePath);

            ret = Convert.ToInt32(ConfigurationManager.AppSettings["DebugDays"]);
            ret = ret == 0 ? -10 : ret * -1;
            dtFlTm = DateTime.Now.AddDays(ret);
            LogPath = Server.MapPath("") + "\\Log\\";

            if (Directory.Exists(LogPath) == false)
                Directory.CreateDirectory(LogPath);

            // Log Debug Files
            string[] sLogs = Directory.GetFiles(LogPath, "Debug*");
            foreach (string sfile in sLogs)
            {
                FileInfo fi = new FileInfo(sfile);
                if (fi.CreationTime <= dtFlTm)
                    fi.Delete();
            }

            // Report Files
            dtFlTm = DateTime.Now.AddDays(-1);
            string[] sRptName = Directory.GetFiles(RptPath);
            foreach (string sfile in sRptName)
            {
                if ((sfile.EndsWith(".pdf") == false) && (sfile.EndsWith(".xlsx") == false))
                    continue;
                FileInfo fi = new FileInfo(sfile);
                if (fi.CreationTime <= dtFlTm)
                    fi.Delete();
            }
        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Session_Start()
        {
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                HttpCookie sessionCookie = Request.Cookies["ASP.NET_SessionId"];
                sessionCookie.Expires = DateTime.Now.AddMinutes(30); // Set an expiry time
                Response.Cookies.Set(sessionCookie);
            }
        }
    }
}
