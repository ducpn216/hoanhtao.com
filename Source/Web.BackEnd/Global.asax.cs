using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.BackEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            DAL.ConnectionConfig.WebConnectionString = WebConfigurationManager.ConnectionStrings["WebSqlConnection"].ConnectionString;
            DAL.ConnectionConfig.InsideConnectionString = WebConfigurationManager.ConnectionStrings["InsideSqlConnection"].ConnectionString;
            //FileLog.FullErrorPath = ConfigConstants.LogFolderPath;

            FileLog.FullErrorPath = HttpContext.Current.Server.MapPath("~/Data/Logs/");
        }
    }
}
