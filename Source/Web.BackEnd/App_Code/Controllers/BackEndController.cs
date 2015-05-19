using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.FrontEnd.App_Code.Controllers
{
    [Authorize]
    public class BackEndController : Controller
    {
        public BackEndController()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    //FormAuthentication
            //}
        }
	}
}