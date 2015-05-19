using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Core
{
    public static class Helper
    {
        public static async Task<Response> CallApi<Response>(string url, int seconds = 5, Enums.Method method = Enums.Method.GET,
            FormUrlEncodedContent formUrlEncodedContent = null, Dictionary<string, string> authorization = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(seconds);

                    if (authorization != null && authorization.Count > 0)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorization.Keys.FirstOrDefault(), authorization.Values.FirstOrDefault());
                    }

                    if (method == Enums.Method.GET)
                    {
                        string response = await httpClient.GetStringAsync(url);
                        FileLog.Write("CallApi", "CallApi", response);
                        if (!string.IsNullOrWhiteSpace(response))
                        {
                            return JsonConvert.DeserializeObject<Response>(response);
                        }
                    }
                    else
                    {
                        var response = httpClient.PostAsync(url, formUrlEncodedContent).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            FileLog.Write("CallApi", "CallApi", result);
                            return JsonConvert.DeserializeObject<Response>(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("CallApi", "CallApi", ex.ToString());
            }

            return default(Response);
        }

        public static async Task<JObject> CallApi(string url, int seconds = 5)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(seconds);

                string response = await httpClient.GetStringAsync(url);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    return JObject.Parse(response);
                }
            }

            return null;
        }

        public static void CallGameApi(string apiDomain, string apiMethod, string gameId,
            string userId, string serverId, string roleId, double timeStamp)
        {
            string.Format("{0}/{1}?gid={2}&sid={3}&user={4}&roleid={5}&time={6}",
                apiDomain, apiMethod, gameId, serverId, userId, roleId, timeStamp);
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                //of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static bool ContainsSpecialChar(string value)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(value, "^[a-zA-Z0-9\x20]+$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string GetErrorsOfModelState(ModelStateDictionary modselState, string split = "<br/>")
        {
            string message = "";

            var errors = modselState.Values.SelectMany(v => v.Errors);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    message += error.ErrorMessage + split;
                }
            }

            return message;
        }
    }
}
