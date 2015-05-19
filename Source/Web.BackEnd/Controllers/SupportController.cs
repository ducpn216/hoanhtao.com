using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code.Attributes;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{
    [CustomAuthorize(ForbiddenGroupId = new[] { 2, 3 })]
    public class SupportController : BackEndController
    {
        public SupportController()
        {

        }

        public ActionResult Index()
        {

            return View();
        }


    }
}