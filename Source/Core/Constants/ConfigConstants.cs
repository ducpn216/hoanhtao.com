using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Core.Constants
{
    public class ConfigConstants
    {
        public const string DOMAIN_SSO = "fanmu.vn";
        public const string CULTURE_VIETNAM = "vi-VN";
        public const string ERROR_MESSAGE = "Có lỗi trong quá trình thực hiện. Xin vui lòng thử lại";
        static public string BACKEND_DOMAIN = ConfigurationManager.AppSettings["DomainAdmin"].ToString();

        public const string NEWS_FOLDER_PATH = "/Data/News/";
        public const string BANNER_FOLDER_PATH = "/Data/Banner/";
        public const string SUPPORT_FOLDER_PATH = "/Data/Support/";
        public const string SUPPORT_DETAIL_FOLDER_PATH = "/Data/SupportDetail/";
        public const string ITEM_FOLDER_PATH = "/Data/Item/";
        //public const string 

        public static readonly string LogFolderPath = HttpContext.Current.Server.MapPath("~/Data/Logs/");
    }
}
