using Core.MVC.Attributes;
using Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.FrontEnd.App_Code.Controllers
{
    //[BasicAuthenticationAttribute("hoanhtao.com", "hoanhtao.com!@#$")]
    public class FrontEndController : Controller
    {
        public FrontEndController()
        {

            //if (Request.QueryString["pid"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["pid"]))
            //{
            //    SessionManager.PID = Request.QueryString["pid"];
            //}
        }
    }
}