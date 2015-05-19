using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core
{
    public class LinkManager
    {
        public static string GetPostListLink(int postTypeId)
        {
            return string.Format("/TinTuc/DanhSach/{0}/", postTypeId);
        }

        public static string GetPostDetailLink(int postId)
        {
            return string.Format("/TinTuc/ChiTiet/{0}", postId);
        }

        public static string GetCamNangLink(int categoryId)
        {
            return string.Format("/CamNang/DanhSach/{0}", categoryId);
        }

        public static string GetCamNangDetailLink(int postId)
        {
            return string.Format("/CamNang/ChiTiet/{0}", postId);
        }

        public static string GetSupportDetailLink(int supportId)
        {
            return string.Format("/ChiTietHotro/{0}", supportId);
        }

        public static string GetLoginLink(string returnUrl)
        {
            return LOGIN + "?returnUrl=" + returnUrl;
        }

        public static string GetLoginJavascriptLink(int serverId)
        {
            return "return playGame(" + serverId + ")";
        }

        public static string GetLoginJavascriptLink(int serverId, bool isHttps = false)
        {
            if (isHttps)
            {
                return "return checkLogin('" + HOME_PAGE_HTTPS + "/play/" + serverId + "')";
            }
            else
            {
                return "return checkLogin('/play/" + serverId + "')";
            }
        }

        public const string HOME_PAGE = "http://hoanhtao.com";
        public const string HOME_PAGE_HTTPS = "https://hoanhtao.com";
        public const string PAYMENT = HOME_PAGE + "/NapThe";
        public const string LOGIN = HOME_PAGE + "/DangNhap";
        public const string LOGOUT = HOME_PAGE + "/Thoat";
        public const string REGISTER = HOME_PAGE + "/DangKy";
        public const string FORGET_PASSWORD = HOME_PAGE + "/QuenMatKhau";
        public const string CHANGE_PASSWORD = HOME_PAGE + "/DoiMatKhau";
        public const string PROFILE = HOME_PAGE + "/HoSo";
        public const string CHOI_NGAY = HOME_PAGE + "/ChoiNgay";
        public const string TRANSFER_GOLD = HOME_PAGE + "/ChuyenTien";

        public const string SUPPORT = HOME_PAGE + "/HoTro";
        public const string SUPPORT_DETAIL = HOME_PAGE + "/ChiTietHoTro";
        public const string SUPPORT_FEEDBACK = HOME_PAGE + "/DangHoTro";

        public const string FANPAGE = "https://www.facebook.com/hoanhtao2015";
        public const string LOGIN_GOOGLE = HOME_PAGE + "/Account/LoginGoogle";
        public const string LOGIN_FACEBOOK = HOME_PAGE + "/Account/LoginFacebook";

        //public const string HOME_PAGE = "http://hoanh.vn";
        //public const string HOME_PAGE_HTTPS = "https://hoanh.vn";
        //public const string PAYMENT = HOME_PAGE + "/nap-the";
        //public const string LOGIN = HOME_PAGE + "/dang-nhap";
        //public const string LOGOUT = HOME_PAGE + "/thoat";
        //public const string REGISTER = HOME_PAGE + "/dang-ky";
        //public const string FORGET_PASSWORD = HOME_PAGE + "/quen-mat-khau";
        //public const string CHANGE_PASSWORD = HOME_PAGE + "/doi-mat-khau";
        //public const string PROFILE = HOME_PAGE + "/thong-tin";
        //public const string CHOI_NGAY = HOME_PAGE + "/choi-ngay";
        //public const string FANPAGE = "https://www.facebook.com/hoanhtaogiangho";
        //public const string LOGIN_GOOGLE = HOME_PAGE + "/Account/LoginGoogle";
        //public const string LOGIN_FACEBOOK = HOME_PAGE + "/Account/LoginFacebook";

        public static string GetPidLink(string link, Guid pid)
        {
            return link.TrimEnd(new char[] { '/' }) + "/?pid=" + pid.ToString();
        }

        public static string GetGameLink(int? serverId = null, bool isHttps = false)
        {
            if (serverId.HasValue)
            {
                return "/Game/" + serverId + "')";
            }
            else
            {
                return "/Game/";
            }
            
        }
    }
}