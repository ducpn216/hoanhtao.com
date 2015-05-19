using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.App_Code
{
    public class SessionManager
    {
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

        public static string GoogleAccessToken
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["GoogleAccessToken"] != null)
                    {
                        return HttpContext.Current.Session["GoogleAccessToken"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["GoogleAccessToken"] != null)
                {
                    HttpContext.Current.Session["GoogleAccessToken"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("GoogleAccessToken", value);
                }
            }
        }

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

        public static string RegisterCaptcha
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["RegisterCaptcha"] != null)
                    {
                        return HttpContext.Current.Session["RegisterCaptcha"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["RegisterCaptcha"] != null)
                {
                    HttpContext.Current.Session["RegisterCaptcha"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("RegisterCaptcha", value);
                }
            }
        }

        public static string LoginCaptcha
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["LoginCaptcha"] != null)
                    {
                        return HttpContext.Current.Session["LoginCaptcha"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["LoginCaptcha"] != null)
                {
                    HttpContext.Current.Session["LoginCaptcha"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("LoginCaptcha", value);
                }
            }
        }

        public static string QuickRegisterCaptcha
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["QuickRegisterCaptcha"] != null)
                    {
                        return HttpContext.Current.Session["QuickRegisterCaptcha"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["QuickRegisterCaptcha"] != null)
                {
                    HttpContext.Current.Session["QuickRegisterCaptcha"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("QuickRegisterCaptcha", value);
                }
            }
        }

        public static string PID
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["PID"] != null)
                    {
                        return HttpContext.Current.Session["PID"].ToString();

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["PID"] != null)
                {
                    HttpContext.Current.Session["PID"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("PID", value);
                }
            }
        }

        public static DateTime? PaymentTime
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["PaymentTime"] != null)
                    {
                        return Convert.ToDateTime(HttpContext.Current.Session["PaymentTime"]); 

                    }
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Session["PaymentTime"] != null)
                {
                    HttpContext.Current.Session["PaymentTime"] = value;
                }
                else
                {
                    HttpContext.Current.Session.Add("PaymentTime", value);
                }
            }
        } 
    }
}