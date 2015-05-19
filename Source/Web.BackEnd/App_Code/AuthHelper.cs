using Core.Constants;
using Core.Providers.Google;
using CustomProviders;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Web.BackEnd.App_Code
{
    public class AuthHelper
    {
        private Uri RedirectUri
        {
            get
            {
                var requestContext = HttpContext.Current.Request.RequestContext;
                //new UrlHelper(requestContext).Action(OauthConstants.FB_CALLBACK, "Account");
                var uriBuilder = new UriBuilder(HttpContext.Current.Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = OauthConstants.FacebookCallback;
                return uriBuilder.Uri;
            }
        }

        public static async Task<bool> CheckLogin()
        {
            bool flag = false;

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                flag = true;
            }

            return flag;
        }

        public static int GetUserId()
        {
            int userId = -1;

            try
            {
                AuthenticationProvider authProvider = new AuthenticationProvider();
                string userData = authProvider.GetUserData();

                if (!string.IsNullOrEmpty(userData))
                {
                    return Convert.ToInt32(authProvider.GetUserData().Split(new string[] { "&&&" }, StringSplitOptions.None)[1]);
                }
            }
            catch (Exception ex)
            {
                userId = -1;
            }

            return userId;
        }

        public static string GetUsername()
        {
            string username = "";

            try
            {
                AuthenticationProvider authProvider = new AuthenticationProvider();
                string userData = authProvider.GetUserData();

                if (!string.IsNullOrEmpty(userData))
                {
                    return authProvider.GetUserData().Split(new string[] { "&&&" }, StringSplitOptions.None)[0];
                }
            }
            catch (Exception ex)
            {
                username = "";
            }

            return username;
        }

        public static int GetGroupId()
        {
            int groupId = -1;

            try
            {
                AuthenticationProvider authProvider = new AuthenticationProvider();
                string userData = authProvider.GetUserData();

                if (!string.IsNullOrEmpty(userData))
                {
                    return Convert.ToInt32(authProvider.GetUserData().Split(new string[] { "&&&" }, StringSplitOptions.None)[2]);
                }
            }
            catch (Exception ex)
            {
                groupId = -1;
            }

            return groupId;
        }
    }
}