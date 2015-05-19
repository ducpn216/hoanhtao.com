using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Web.FrontEnd.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Teaser
            bundles.Add(new StyleBundle("~/teaser/css").Include(
               "~/Content/views/landing/style.css"
           ));

            bundles.Add(new ScriptBundle("~/teaser/js").Include(
                        "~/Content/js/jquery-1.11.2.min.js",
                        "~/Content/js/MyLibrary.js"
                        ));
            #endregion

            #region home
            bundles.Add(new StyleBundle("~/home/css").Include(
                "~/Content/libraries/bootstrap-3.3.4/css/bootstrap.min.css",
                "~/Content/libraries/bootstrap-3.3.4/css/bootstrap-theme.min.css",
                "~/Content/css/screen.css",
                "~/Content/css/style.css",
                "~/Content/css/custom.css"
            ));

            bundles.Add(new ScriptBundle("~/home/js").Include(
                        "~/Content/js/jquery-1.11.2.min.js",
                        "~/Content/js/fadegallery.js",
                        "~/Content/js/iOS.js",
                        "~/Content/js/custom-form-elements.js",
                        "~/Content/js/jquery-ui.js",
                        "~/Content/js/function.js",
                        "~/Content/js/script.js",
                        "~/Content/libraries/bootstrap-3.3.4/js/bootstrap.min.js",
                        "~/Content/js/MyLibrary.js"
                        ));
            #endregion

            #region inside
            bundles.Add(new StyleBundle("~/inside/css").Include(
                "~/Content/libraries/bootstrap-3.3.4/css/bootstrap.min.css",
                "~/Content/css/inside/screen.css",
                "~/Content/css/custom.css"
            ));

            bundles.Add(new ScriptBundle("~/inside/js").Include(
                        "~/Content/js/jquery-1.11.2.min.js",
                        "~/Content/js/jquery-ui.js",
                        "~/Content/js/jquery.roundabout2.js",
                        "~/Content/js/jquery.roundabout-shapes2.js",
                        "~/Content/js/jquery.numeric.min.js",
                        "~/Content/libraries/bootstrap-3.3.4/js/bootstrap.min.js",
                        "~/Content/js/myFunction.js",
                        "~/Content/js/function.js",
                        "~/Content/js/MyLibrary.js"
                        ));
            #endregion
            // Code removed for clarity.
        }
    }
}