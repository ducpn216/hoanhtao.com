using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.App_Code
{
    public class CacheManager
    {
        public CacheManager()
        {

        }

        public static string FacebookAccessToken
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["FacebookAccessToken"] != null)
                    {
                        return HttpContext.Current.Session["FacebookAccessToken"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["FacebookAccessToken"] != null)
                {
                    HttpContext.Current.Session["FacebookAccessToken"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("FacebookAccessToken", value);
                }
            }
        }
    }
}