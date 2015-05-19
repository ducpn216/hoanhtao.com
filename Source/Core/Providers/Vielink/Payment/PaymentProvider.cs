using Core.Constants;
using Core.Providers.Payment.Requests;
using Core.Providers.Payment.Responses;
using Core.Providers.Vielink.Payment.Responses;
using Core.VielinkPayment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Vielink.Payment
{
    public class PaymentProvider
    {
        public async Task<List<CardResponse>> GetCardList()
        {
            List<CardResponse> list = null;
            try
            {
                string url = PaymentConstants.IP + "/ver2.0/?r=common/Getcardtype";
                string password = CommonProvider.SecurityProvider.MD5_Encrypt(PaymentConstants.CLIENT_KEY + PaymentConstants.CLIENT_NAME);

                var authorize = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", PaymentConstants.CLIENT_NAME, password)));

                Dictionary<string, string> authorization = new Dictionary<string, string>();
                authorization.Add("Basic", authorize);

                list = await Helper.CallApi<List<CardResponse>>(url, 5, Enums.Method.GET, null, authorization);
            }
            catch (Exception ex)
            {
                FileLog.Write("Payment", "CarList", ex.ToString());
            }

            return list;
        }

        public ChargeResponse Charge(string serial, string pin, string cardType, string username)
        {
            ChargeResponse chargeResponse = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(serial) && !string.IsNullOrWhiteSpace(pin) &&
                !string.IsNullOrWhiteSpace(cardType) && !string.IsNullOrWhiteSpace(username))
                {
                    string key = cardType + pin + PaymentConstants.CLIENT_NAME + PaymentConstants.CLIENT_KEY;
                    string request = JsonConvert.SerializeObject(new ChargeRequest()
                    {
                        merchantCode = PaymentConstants.CLIENT_NAME,
                        cardtype = cardType,
                        cardserial = serial,
                        cardcode = pin,
                        signkey = Encrypt(PaymentConstants.CLIENT_KEY, key),
                        target = username
                    });

                    if (!string.IsNullOrEmpty(request))
                    {
                        using (CardChargingControllerServicePortTypeClient service = new CardChargingControllerServicePortTypeClient())
                        {
                            string response = service.CardChargingControllerUseCard(request);
                            chargeResponse = JsonConvert.DeserializeObject<ChargeResponse>(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Payment", "Charge", ex.ToString());
            }
            
            return chargeResponse;
        }

        public string GetErrorMessage(string key)
        {
            string message = "";

            switch (key)
            {
                case "-18":
                    message = "Partner không tồn tại";
                    break;
                case "-11":
                    message = "Xác thực không hợp lệ";
                    break;
                case "-15":
                    message = "Sai địa chỉ IP ";
                    break;
                case "99":
                    message = "Lỗi không xác định.";
                    break;
                case "00":
                    message = "Mã số nạp tiền không tồn tại hoặc đã được sử dụng  ";
                    break;
                case "03":
                    message = "Thẻ đã được sử dụng  ";
                    break;
                case "04":
                    message = "Thẻ đã bị khóa ";
                    break;
                case "05":
                    message = "Thẻ đã hết hạn sử dụng  ";
                    break;
                case "06":
                    message = "Thẻ chưa được kích hoạt";
                    break;
                case "07":
                    message = "Thực hiện sai quá số lần cho phép";
                    break;
                case "08":
                    message = "Giao dịch nghi vấn (Timeout từ telco, chưa xử lý xong)";
                    break;
                case "09":
                    message = "Sai định dạng thông tin truyền vào";
                    break;
                case "13":
                    message = "Hệ thống của Đơn vị phát hành đang bận";
                    break;
                case "20":
                    message = "Sai độ dài mã số nạp tiền";
                    break;
                case "21":
                    message = "Mã giao dịch không hợp lệ (>0 và < 30 ký tự)";
                    break;
                case "23":
                    message = "Serial thẻ không hợp lệ (áp dung cho thẻ VT)";
                    break;
                case "24":
                    message = "Mã số nạp tiền và serial không khớp (áp dung cho thẻ VT)";
                    break;
                case "25":
                    message = "Trùng mã giao dịch";
                    break;
                case "26":
                    message = "Mã giao dịch không tồn tại";
                    break;
                case "28":
                    message = "Mã số nạp tiền không đúng định dạng (chỉ bao gồm ký tự số)";
                    break;
                case "40":
                    message = "Lỗi kết nối tới Đơn vị phát hành – Nhà cung cấp thẻ";
                    break;
                case "41":
                    message = "Lỗi khi Đơn vị phát hành xử lý giao dịch";
                    break;
                case "51":
                    message = "Đơn vị phát hành không tồn tại";
                    break;
                case "52":
                    message = "Đơn vị phát hành không hỗ trợ nghiệp vụ này";
                    break;
                //case "99":
                //    message = "Lỗi không xác định khi xử lý giao dịch";
                //    break;
                case "1":
                    message = "Giao dịch thành công";
                    break;
                case "-1":
                    message = "Thẻ đã sử dụng";
                    break;
                case "-2":
                    message = "Thẻ đã khóa ";
                    break;
                case "-3":
                    message = "Thẻ đã hết hạn sử dụng";
                    break;
                case "-4":
                    message = "Mã thẻ chưa được kích hoạt";
                    break;
                case "-10":
                    message = "Mã thẻ sai định dạng";
                    break;
                case "-12":
                    message = "Thẻ không tồn tại";
                    break;
                case "-99":
                    message = "Lỗi thẻ VMS(MOBI) không đúng định dạng";
                    break;
                case "5":
                    message = "Nhập sai mã thẻ quá 5 lần";
                    break;
                    //case "99":
                    //    message = "Chưa nhận được kết quả trả về từ nhà cung cấp mã thẻ";
                    break;
                case "-24":
                    message = "Dữ liệu carddata không đúng";
                    break;
                //case "-11":
                //    message = "Nhà cung cấp không tồn tại";
                //    break;
                case "4":
                    message = "Thẻ không sử dụng được";
                    break;
                //case "13":
                //    message = "Hệ thống tạm thời bận";
                //    break;
                case "0":
                    message = "Transaction fail";
                    break;
                case "9":
                    message = "Tạm  thời khóa kênh nạp VMS(MOBI) do quá tải";
                    break;
                case "10":
                    message = "Hệ thống nhà cung cấp gặp lỗi";
                    break;
                case "11":
                    message = "Nhà cung cấp tạm thời khóa do lỗi hệ thống";
                    break;
                case "12":
                    message = "Trùng transaction id";
                    break;
                case "50":
                    message = "Thẻ đã sử dụng hoặc không tồn tại";
                    break;
                //case "51":
                //    message = "Seri thẻ không đúng";
                //    break;
                //case "52":
                //    message = "Mã thẻ và serial không khớp";
                //    break;
                case "53":
                    message = "Mã thẻ và serial không khớp";
                    break;
                case "54":
                    message = "Card chưa được kích hoạt, liên hệ với nhà cung cấp";
                    break;
                case "55":
                    message = "Card tạm thời bị block 24 h";
                    break;
                case "59":
                    message = "Mã thẻ chưa được kích hoạt";
                    break;
                default:
                    break;
            }

            return message;
        }

        public string Encrypt(string key, string data)
        {
            data = data.Trim();
            byte[] keydata = Encoding.ASCII.GetBytes(key);
            string md5String = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(keydata)).Replace("-", "").ToLower();
            byte[] tripleDesKey = Encoding.ASCII.GetBytes(md5String.Substring(0, 24));

            TripleDES tripdes = TripleDESCryptoServiceProvider.Create();
            tripdes.Mode = CipherMode.ECB;
            tripdes.Key = tripleDesKey;
            tripdes.GenerateIV();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream encStream = new CryptoStream(ms, tripdes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    encStream.Write(Encoding.ASCII.GetBytes(data), 0, Encoding.ASCII.GetByteCount(data));
                    encStream.FlushFinalBlock();
                    byte[] cryptoByte = ms.ToArray();

                    ms.Close();
                    encStream.Close();

                    return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0)).Trim();
                }
            }
        }
    }
}
