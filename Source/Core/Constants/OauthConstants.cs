using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Core.Constants
{
    public class OauthConstants
    {
        public static string FacebookAppId
        {
            get
            {
                return WebConfigurationManager.AppSettings["FacebookAppId"];
            }
        }

        public static string FacebookAppSecret
        {
            get
            {
                return WebConfigurationManager.AppSettings["FacebookAppSecret"];
            }
        }

        public static string FacebookCallback
        {
            get
            {
                var request = HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}{3}",
                                    request.Url.Scheme,
                                    request.Url.Host,
                                    request.Url.Port == 80 ? string.Empty : ":" + request.Url.Port,
                                    WebConfigurationManager.AppSettings["FacebookCallBack"]);
            }
        }

        public static string GoogleAppId
        {
            get
            {
                return WebConfigurationManager.AppSettings["GoogleAppId"];
            }
        }

        public static string GoogleAppSecret
        {
            get
            {
                return WebConfigurationManager.AppSettings["GoogleAppSecret"];
            }
        }

        public static string GoogleCallback
        {
            get
            {
                var request = HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}{3}",
                                    request.Url.Scheme,
                                    request.Url.Host,
                                    request.Url.Port == 80 ? string.Empty : ":" + request.Url.Port,
                                    WebConfigurationManager.AppSettings["GoogleCallback"]);
            }
        }

    }
}
