using Core.API.GameApiRequests;
using Core.API.GameApiResponses;
using Core.GameAPI.Requests;
using Core.GameAPI.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Core.API
{
    public class GameAPI
    {
        readonly string BASE_URL = ConfigurationManager.AppSettings["ApiGameUrl"];
        readonly string BASE_CHARGE_URL = ConfigurationManager.AppSettings["ApiGameChargeUrl"];
        readonly string KEY = ConfigurationManager.AppSettings["ApiGameKey"];
        readonly string TEST_GAME_PLAY_URL = ConfigurationManager.AppSettings["TestGamePlayUrl"];
        readonly string REAL_GAME_PLAY_URL = ConfigurationManager.AppSettings["RealGamePlayUrl"];
        readonly string M_KEY = "hstx";

        public async Task<List<RoleListResponse>> GetRoleList(RoleListRequest request)
        {
            string url = BASE_URL + String.Format("?op=role_list&user_id={0}&server_id={1}&account_id={2}&page={3}&page_size={4}",
                request.UserId, request.ServerId, request.AccountId, request.Page, request.PageSize);

            List<RoleListResponse> result = await Helper.CallApi<List<RoleListResponse>>(url);
            if (result != null)
            {
                return result as List<RoleListResponse>;
            }

            return null;
        }

        public async Task<List<RoleListResponse>> GetRoleList(int accountId, int serverId)
        {
            string url = BASE_URL + String.Format("?op=role_list&account_id={0}&server_id={1}&page=1&page_size=2", accountId, serverId);

            List<RoleListResponse> result = await Helper.CallApi<List<RoleListResponse>>(url);
            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<OnlineNumberResponse> GetOnlineNumber(int serverId)
        {
            string url = BASE_URL + String.Format("?op=onlinenum&server_id={0}", serverId);

            OnlineNumberResponse result = await Helper.CallApi<OnlineNumberResponse>(url);
            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<LockAccountResponse> LockAccount(LockAccountRequest request)
        {
            string url = BASE_URL + String.Format("?op=lock_player&user_id={0}&server_id={1}&role_id={2}&role_name={3}&time={4}&reason={5}",
                request.UserId, request.ServerId, request.RoleId, request.RoleName, request.LockTime, request.Reason);

            FileLog.Write("GameAPI", "LockAccount", url);

            LockAccountResponse result = await Helper.CallApi<LockAccountResponse>(url);
            if (result != null)
            {
                return result as LockAccountResponse;
            }

            return null;
        }

        public async Task<AddExpResponse> AddExp(AddExpRequest request)
        {
            string url = BASE_URL + String.Format("?op=add_exp&user_id={0}&server_id={1}&role_id={2}&exp={3}",
                request.UserId, request.ServerId, request.RoleId, request.Exp);

            FileLog.Write("GameAPI", "AddExp", url);

            AddExpResponse result = await Helper.CallApi<AddExpResponse>(url);
            if (result != null)
            {
                return result as AddExpResponse;
            }

            return null;
        }

        public async Task<DeleteItemResponse> DeleteItem(DeleteItemRequest request)
        {
            string url = BASE_URL + String.Format("?op=del_item&user_id={0}&server_id={1}&role_id={2}&inst_id={3}",
                request.UserId, request.ServerId, request.RoleId, request.ItemId);

            FileLog.Write("GameAPI", "DeleteItem", url);

            DeleteItemResponse result = await Helper.CallApi<DeleteItemResponse>(url);
            if (result != null)
            {
                return result as DeleteItemResponse;
            }

            return null;
        }

        public async Task<JumpMapResponse> JumpMap(JumpMapRequest request)
        {
            string url = BASE_URL + String.Format("?op=map_jump&user_id={0}&server_id={1}&role_id={2}&map={3}&x={4}&y={5}",
                request.UserId, request.ServerId, request.RoleId, request.MapId, request.X, request.Y);

            FileLog.Write("GameAPI", "JumpMap", url);

            JumpMapResponse result = await Helper.CallApi<JumpMapResponse>(url);
            if (result != null)
            {
                return result as JumpMapResponse;
            }

            return null;
        }

        public async Task<BanChatResponse> BanChat(BanChatRequest request)
        {
            string url = BASE_URL + String.Format("?op=chat_ban&user_id={0}&server_id={1}&role_id={2}&last={3}",
                request.UserId, request.ServerId, request.RoleId, request.Seconds);

            FileLog.Write("GameAPI", "BanChat", url);

            BanChatResponse result = await Helper.CallApi<BanChatResponse>(url);
            if (result != null)
            {
                return result as BanChatResponse;
            }

            return null;
        }

        public async Task<KickOfflineResponse> KickOffline(KickOfflineRequest request)
        {
            string url = BASE_URL + String.Format("?op=kick_off&user_id={0}&server_id={1}&role_id={2}",
                request.UserId, request.ServerId, request.RoleId);

            FileLog.Write("GameAPI", "KickOffline", url);

            KickOfflineResponse result = await Helper.CallApi<KickOfflineResponse>(url);
            if (result != null)
            {
                return result as KickOfflineResponse;
            }

            return null;
        }

        public async Task<CharDetailResponse> GetCharDetail(CharDetailRequest request)
        {
            string url = BASE_URL + String.Format("?op=game_data&user_id={0}&server_id={1}&role_id={2}&type={3}&page={4}&page_size={5}",
                request.UserId, request.ServerId, request.RoleId, (int)request.Type, request.Page, request.PageSize);

            FileLog.Write("GameAPI", "GetCharDetail", url);

            CharDetailResponse result = await Helper.CallApi<CharDetailResponse>(url);
            if (result != null)
            {
                return result as CharDetailResponse;
            }

            return null;
        }

        public async Task<ModifyMallResponse> ModifyMall(ModifyMallRequest request)
        {
            string json = JsonConvert.SerializeObject(request.Data);

            string url = BASE_URL + String.Format("?op=change_mall&server_id={0}&data={1}", request.ServerId, json);

            FileLog.Write("GameAPI", "ModifyMall", url);

            ModifyMallResponse result = await Helper.CallApi<ModifyMallResponse>(url);
            if (result != null)
            {
                return result as ModifyMallResponse;
            }

            return null;
        }

        public async Task<SendMailResponse> SendMail(SendMailRequest request)
        {
            try
            {
                string url = BASE_URL + "?op=send_mail" +
                "&user_id=" + request.UserId +
                "&server_id=" + request.ServerId +
                "&sender_id=" + request.SenderId +
                "&sender_name=" + request.SenderName +
                "&role_id=" + request.RoleId +
                "&all=" + (int)request.SendAll +
                "&title=" + request.Title +
                "&content=" + request.Content +
                "&gold=" + request.Gold +
                "&bind_gold=" + request.BindGold +
                "&money=" + request.Money;

                if (!string.IsNullOrEmpty(request.ItemId1) && request.NumberOfItem1 > 0)
                {
                    url += "&item1=" + request.ItemId1 + "&num1=" + request.NumberOfItem1;
                }

                if (!string.IsNullOrEmpty(request.ItemId2) && request.NumberOfItem2 > 0)
                {
                    url += "&item2=" + request.ItemId2 + "&num2=" + request.NumberOfItem2;
                }

                if (!string.IsNullOrEmpty(request.ItemId3) && request.NumberOfItem3 > 0)
                {
                    url += "&item3=" + request.ItemId3 + "&num3=" + request.NumberOfItem3;
                }

                if (!string.IsNullOrEmpty(request.ItemId4) && request.NumberOfItem4 > 0)
                {
                    url += "&item=4" + request.ItemId4 + "&num4=" + request.NumberOfItem4;
                }

                if (!string.IsNullOrEmpty(request.ItemId5) && request.NumberOfItem5 > 0)
                {
                    url += "&item5=" + request.ItemId5 + "&num5=" + request.NumberOfItem5;
                }

                FileLog.Write("GameAPI", "SendMail", url);
                SendMailResponse result = await Helper.CallApi<SendMailResponse>(url);
                if (result != null)
                {
                    return result as SendMailResponse;
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("GameAPI", "SendMail", ex.ToString());
            }


            return null;
        }

        public async Task<SendNoticeResponse> SendNotice(SendNoticeRequest request)
        {
            string url = BASE_URL + String.Format("?op=send_notice&user_id={0}&server_id={1}&notice={2}&link={3}",
                request.UserId, request.ServerId, request.Notice, request.Link);

            FileLog.Write("GameAPI", "SendNotice", url);

            SendNoticeResponse result = await Helper.CallApi<SendNoticeResponse>(url);
            if (result != null)
            {
                return result as SendNoticeResponse;
            }

            return null;
        }

        public async Task<int> Charge(ChargeRequest request)
        {
            try
            {
                string time = Math.Floor((double)(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();

                string url = "op=charge" +
                    "&server_id=" + request.ServerId +
                    "&account=" + request.AccountId +
                    "&order_form=" + request.TransId +
                    "&quantity=" + request.GamePoint +
                    "&type=1" +
                    "&time=" + time;
                string orginalData = url + "&key=" + M_KEY;
                string sign = GenHash(orginalData);

                FileLog.Write("GameAPI", "Charge", url);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("op", "charge"),
                    new KeyValuePair<string, string>("server_id", request.ServerId.ToString()),
                    new KeyValuePair<string, string>("account", request.AccountId.ToString()),
                    new KeyValuePair<string, string>("order_form", request.TransId.ToString()),
                    new KeyValuePair<string, string>("quantity", request.GamePoint.ToString()),
                    new KeyValuePair<string, string>("type", "1"),
                    new KeyValuePair<string, string>("time", time.ToString()),
                    new KeyValuePair<string, string>("sign", sign.ToString())
                });

                int result = await Helper.CallApi<int>(BASE_CHARGE_URL, 5, Enums.Method.POST, formContent);

                return 1;

            }
            catch (Exception ex)
            {
                FileLog.Write("GameAPI", "Charge", ex.ToString());
            }


            return -1;
        }
        public string GetGameUrl(int accountId, string username, string serverId, bool isReal = true, bool isHttps = false)
        {
            string url = "";
            string gw = "1";
            string uf = "hoanh_tao_com";
            string time = Math.Floor((double)(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();

            username = username.ToLower();

            string sign = GenHash(gw + accountId + time + KEY);
            //string baseUrl = isReal ? REAL_GAME_PLAY_URL : TEST_GAME_PLAY_URL;
            //string baseUrl = isReal ? "http://s" + Convert.ToInt32(serverId).ToString("000") + ".hoanhtao.com/index.html" : TEST_GAME_PLAY_URL;
            string baseUrl = isReal ? "http://s" + serverId + ".hoanhtao.com/index.html" : "http://s999.hoanhtao.com/index.html";

            if (isHttps)
            {
                baseUrl = baseUrl.Replace("http", "https");
            }

            url = baseUrl + string.Format("?gw={0}&time={1}&uf={2}&uid={3}&sid={4}&sign={5}",
                            gw, time, uf, accountId, Convert.ToInt32(serverId).ToString("000"), sign);

            return url;
        }

        public string GenHash(string EncString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = encoding.GetBytes(EncString);
            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", null).ToLower();
        }
    }
}
