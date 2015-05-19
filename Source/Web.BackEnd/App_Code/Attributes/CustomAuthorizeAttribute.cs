using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.BackEnd.App_Code.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }
        public string ReturnUrl { get; set; }
        public int[] ForbiddenGroupId { get; set; }

        public CustomAuthorizeAttribute()
        {

        }

        public CustomAuthorizeAttribute(string returnUrl)
        {
            this.ReturnUrl = returnUrl;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                int groupId = AuthHelper.GetGroupId();
                if (CheckExist(groupId))
                {
                    return false;
                }
            }
            if (AuthHelper.CheckLogin().Result)
            {
                return true;
            }
            else
            {
                return false;
            }
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //{
            //    return false;
            //}

            //string privilegeLevels = string.Join("", GetUserRights(httpContext.User.Identity.Name.ToString())); // Call another method to get rights of the user from DB

            //if (privilegeLevels.Contains(this.AccessLevel))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                int groupId = AuthHelper.GetGroupId();
                if (CheckExist(groupId))
                {
                    filterContext.Result = new RedirectResult("/");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult(LinkManager.GetLoginLink(ReturnUrl));
            }

            //filterContext.Result = new RedirectToRouteResult()
            //            new RouteValueDictionary(
            //                new
            //                {
            //                    controller = "Account",
            //                    action = "Login"
            //                })
            //            );
            //filterContext.Result = filterContext.Controller.ControllerContext.HttpContext.. new RedirectToAction
        }

        bool CheckExist(int groupId)
        {
            for (int i = 0; i < ForbiddenGroupId.Length; i++)
            {
                if (groupId == ForbiddenGroupId[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}