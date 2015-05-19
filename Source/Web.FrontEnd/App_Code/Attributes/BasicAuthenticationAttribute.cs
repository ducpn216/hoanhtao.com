using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.FrontEnd.App_Code.Attributes
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public string BasicRealm { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        public BasicAuthenticationAttribute(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;

            string authHeader = request.Headers["Authorization"];

            if ((authHeader != null) && (authHeader.StartsWith("Basic")))
            {
                // Parse username and password out of the HTTP headers
                authHeader = authHeader.Substring("Basic".Length).Trim();
                byte[] authHeaderBytes = Convert.FromBase64String(authHeader);
                authHeader = Encoding.UTF7.GetString(authHeaderBytes);
                string userName = authHeader.Split(':')[0];
                string password = authHeader.Split(':')[1];

                // Validate login attempt
                if (userName == Username && password == Password)
                {
                    return;
                }
            }

            response.StatusCode = 401;
            response.AppendHeader("WWW-Authenticate", "Basic");
            response.End();

            //var req = filterContext.HttpContext.Request;
            //var auth = req.Headers["Authorization"];
            //if (!String.IsNullOrEmpty(auth))
            //{
            //    var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
            //    var user = new { Name = cred[0], Pass = cred[1] };
            //    if (user.Name == Username && user.Pass == Password) return;
            //}
            //var res = filterContext.HttpContext.Response;
            //res.StatusCode = 401;
            //res.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel"));
            //res.End();
        }
    }
}