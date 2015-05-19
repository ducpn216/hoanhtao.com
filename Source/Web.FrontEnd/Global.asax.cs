using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.FrontEnd.App_Start;

namespace Web.FrontEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true; 

            DAL.ConnectionConfig.WebConnectionString = WebConfigurationManager.ConnectionStrings["WebSqlConnection"].ConnectionString;
            DAL.ConnectionConfig.InsideConnectionString = WebConfigurationManager.ConnectionStrings["InsideSqlConnection"].ConnectionString;

            FileLog.FullErrorPath = HttpContext.Current.Server.MapPath("~/Data/Logs/");
        }
    }
}
