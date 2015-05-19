using Core.Providers.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Core.Providers
{
    public class PaymentProvider
    {
        readonly string BASE_URL = WebConfigurationManager.AppSettings["PaymentBaseUrl"];

        public Dictionary<string, string> GetCardList()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("VNP", "Vinaphone");
            dictionary.Add("VMS", "Mobiphone");
            dictionary.Add("VTT", "Viettel");

            return dictionary;
        }

        public async Task<PaymentResponse> Charge(PaymentRequest request)
        {
            string url = BASE_URL + String.Format("/sspay/service.php?pin={0}&serial={1}&provider={2}&product_id={3}",
                request.Pin, request.Serial, request.Provider, request.ProductId);

            FileLog.Write("Payment", "Charge", url);

            PaymentResponse result = await Helper.CallApi<PaymentResponse>(url, 20);
            return result;
        }

        public string GetErrorMessage(int status)
        {
            string message = "";

            switch (status)
            {
                case -24:
                    message = "Dữ liệu không đúng";
                    break;
                case -11:
                    message = "Nhà cung cấp không hợp lệ";
                    break;
                case -10:
                    message = "Thẻ không hợp lệ";
                    break;
                case 0:
                    message = "Nạp thẻ thất bại";
                    break;
                case 1:
                    message = "Nạp thẻ thành công";
                    break;
                case 3:
                    message = "Session ID không đúng";
                    break;
                case 4:
                    message = "Thông tin thẻ không phù hợp";
                    break;
                case 5:
                    message = "Thông tin thẻ không phù hợp";
                    break;
                case 7:
                    message = "Session Time out";
                    break;
                case 8:
                    message = "IP không hợp lệ";
                    break;
                case 9:
                    message = "";
                    break;
                case 10:
                    message = "Nhà cung cấp không hợp lệ";
                    break;
                case 11:
                    message = "Telco";
                    break;
                case 12:
                    message = "Transaction duplicate";
                    break;
                case 13:
                    message = "Hệ thống bận";
                    break;
                case -2:
                    message = "Thẻ bị khóa";
                    break;
                case -3:
                    message = "Thẻ hết hạn sử dụng";
                    break;
                case 50:
                    message = "Thẻ đã sử dụng hoặc không tồn tại";
                    break;
                case 51:
                    message = "Serial không đúng";
                    break;
                case 52:
                    message = "Serial và Pin không phù hợp";
                    break;
                case 53:
                    message = "Serial hoặc Pin không phù hợp";
                    break;
                case 55:
                    message = "Thẻ bị khóa trong vòng 24h";
                    break;
                case 62:
                    message = "Sai mật khẩu";
                    break;
                case 57:
                    message = "Sai mpin";
                    break;
                case 58:
                    message = "Sai tham số";
                    break;
                case 59:
                    message = "Thẻ chưa kích hoạt";
                    break;
                case 60:
                    message = "Sai PartnerId";
                    break;
                case 61:
                    message = "Sai UserId";
                    break;
                case 56:
                    message = "";
                    break;
                case 63:
                    message = "";
                    break;
                case 64:
                    message = "";
                    break;
                case 65:
                    message = "";
                    break;
                case 99:
                    message = "Giao dịch bị pending";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
