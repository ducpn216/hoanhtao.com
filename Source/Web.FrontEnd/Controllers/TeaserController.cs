using Core.MVC.Attributes;
using Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.FrontEnd.Controllers
{
    //[BasicAuthentication("sieunhan", "sieunhan1234")]
    public class TeaserController : FrontEndController
    {
        public TeaserController()
        {

        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}