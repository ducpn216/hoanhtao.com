using Core.Constants;
using Core.Providers.Google;
using CustomProviders;
using Facebook;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Web.FrontEnd.App_Code
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

            if (!string.IsNullOrEmpty(SessionManager.FacebookAccessToken))
            {
                try
                {
                    var client = new FacebookClient(SessionManager.FacebookAccessToken);
                    client.AccessToken = SessionManager.FacebookAccessToken;

                    dynamic result = client.Get("me?fields=id,email");
                    var email = (string)result.email;

                    flag = true;
                }
                catch (FacebookOAuthException)
                {
                    flag = false;
                }
            }
            else if (!string.IsNullOrEmpty(SessionManager.GoogleAccessToken))
            {
                GoogleProvider googleProvider = new GoogleProvider();
                var result = await googleProvider.GetProfile(SessionManager.GoogleAccessToken);
                if (result != null)
                {
                    flag = true;
                }
            }
            else if (HttpContext.Current.Request.IsAuthenticated)
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

        public static bool CheckAdmin(string username)
        {
            bool flag = false;

            List<string> accountList = new List<string>() { "ducpn216", "duyhnq", "alexdeptrai", "mrjustyle", "nymphetm", 
            "test1104", "test1103"};

            foreach (string account in accountList)
            {
                if (account == username)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
    }
}