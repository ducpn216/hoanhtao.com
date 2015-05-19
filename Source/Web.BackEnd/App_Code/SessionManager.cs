using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.BackEnd.App_Code
{
    public class SessionManager
    {
        public static string Captcha
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["Captcha"] != null)
                    {
                        return HttpContext.Current.Session["Captcha"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["Captcha"] != null)
                {
                    HttpContext.Current.Session["Captcha"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("Captcha", value);
                }
            }
        }
    }
}