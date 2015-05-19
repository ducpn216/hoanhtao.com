using Core.Constants;
using Core.Providers.Google.Requests;
using Core.Providers.Google.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Providers.Google
{
    public class GoogleProvider
    {
        const string GOOGLE_OAUTH_URL = "https://accounts.google.com/o/oauth2/auth";
        const string GOOGLE_API_URL = "https://www.googleapis.com";
        const string GOOGLE_ACCESS_TOKEN = "http://www.googleapis.com/oauth2/v3/token";

        public string GetLoginUrl(TokenRequest request)
        {
            string url = string.Format(GOOGLE_OAUTH_URL + "?response_type=code" +
                "&client_id={0}" +
                "&redirect_uri={1}" +
                "&scope={2}" +
                "&approval_prompt={3}&access_type=online",
                OauthConstants.GoogleAppId, HttpUtility.UrlEncode(OauthConstants.GoogleCallback),
                 request.Scope, request.ApprovalPrompt);

            FileLog.Write("Google", "GetLoginUrl", url);

            return url;
        }

        public async Task<string> GetAccessToken()
        {
            try
            {
                var request = HttpContext.Current.Request;
                string code = request.QueryString["code"];

                if (!string.IsNullOrEmpty(code))
                {

                    string url = string.Format(GOOGLE_API_URL + "/oauth2/v3/token/?code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code",
                    code, OauthConstants.GoogleAppId, OauthConstants.GoogleAppSecret, HttpUtility.UrlEncode(OauthConstants.GoogleCallback));
                    //FileLog.Write("Google", "AccessToken", url);
                    //var content = new FormUrlEncodedContent(new[] 
                    //{
                    //    new KeyValuePair<string, string>("code", code),
                    //    new KeyValuePair<string, string>("client_id", OauthConstants.GoogleAppId),
                    //    new KeyValuePair<string, string>("client_secret", OauthConstants.FacebookAppSecret),
                    //    new KeyValuePair<string, string>("redirect_uri",HttpUtility.UrlEncode( OauthConstants.GoogleCallback)),
                    //    new KeyValuePair<string, string>("grant_type", code)
                    //});

                    AccessTokenResponse response = await Helper.CallApi<AccessTokenResponse>(url, 5, Enums.Method.POST, null);
                    return response.access_token;
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Google", "AccessToken", ex.ToString());
            }

            return "";
        }

        public async Task<ProfileResponse> GetProfile(string accessToken)
        {
            ProfileResponse response = null;

            try
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    string url = GOOGLE_API_URL + "/plus/v1/people/me?access_token=" + accessToken;

                    //Dictionary<string, string> authorization = new Dictionary<string, string>();
                    //authorization.Add("Bearer", accessToken);

                    response = await Helper.CallApi<ProfileResponse>(url, 5, Enums.Method.GET, null, null);
                }
                
            }
            catch (Exception ex)
            {
                response = null;
            }

            return response;
        }

        //public bool CheckAccessTokenValid(string accessToken)
        //{
        //    https://www.googleapis.com/plus/v1/people/me?access_token=ya29.UAGHqm70VkR6CXtdEu_XoYvYpII-RfNCY5xDJwG82XYrlHy1f4M5g6hYeWkkLEoMYtZausqw69NtgA
        //}
    }
}
