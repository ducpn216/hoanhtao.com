using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Utilities
    {

        public static string GetPostTypeName(int postTypeId)
        {
            string value = "";

            switch (postTypeId)
            {
                case (int)Enums.PostType.News:
                    value = "Tin tức";
                    break;
                case (int)Enums.PostType.Event:
                    value = "Sự kiện";
                    break;
                case (int)Enums.PostType.CamNang:
                    value = "Cẩm nang";
                    break;
                case (int)Enums.PostType.FAQ:
                    value = "FAQ";
                    break;
                //case (int)Enums.PostType.GioiThieu:
                //    value = "Giới thiệu";
                //    break;
                //case (int)Enums.PostType.TanThu:
                //    value = "Tân thủ";
                //    break;
                default:
                    break;
            }

            return value;
        }

        public static string GetSupportStatusName(int status)
        {
            string str = "";

            switch (status)
            {
                case (int)Enums.SupportStatus.Resolved:
                    str = "Đã xử lý";
                    break;
                case (int)Enums.SupportStatus.NotResolve:
                    str = "Chưa xử lý";
                    break;
                case (int)Enums.SupportStatus.Closed:
                    str = "Đã đóng";
                    break;
                default:
                    break;
            }

            return str;
        }

        public static string GetBannerPositionName(int status)
        {
            string str = "";

            switch (status)
            {
                case (int)Enums.BannerTypes.Main:
                    str = "Event Banner";
                    break;
                case (int)Enums.BannerTypes.Launcher:
                    str = "Launcher";
                    break;
                //case (int)Enums.BannerTypes.HomeEvent:
                //    str = "Event trang chủ";
                //    break;
                default:
                    break;
            }

            return str;
        }

        public static string GetServerStatusName(Enums.ServerStatus status)
        {
            string str = "";

            switch (status)
            {
                case Enums.ServerStatus.Hot:
                    str = "HOT";
                    break;
                case Enums.ServerStatus.New:
                    str = "NEW";
                    break;
                default:
                    break;
            }

            return str;
        }

        public static string GetSellStatus(Enums.SellStatus status)
        {
            string str = "";

            switch (status)
            {
                case Enums.SellStatus.HaKe:
                    str = "Hạ kệ";
                    break;
                case Enums.SellStatus.LenKe:
                    str = "Lên kệ";
                    break;
                default:
                    str = "Không thay đổi";
                    break;
            }

            return str;
        }

        public static bool IsValidTestUser(string username)
        {
            string[] array = { "ducpn216", "homvey", "duyhnq1012", "kyanh123", "xuanhai184" };

            foreach (string item in array)
            {
                if (username == item)
                {
                    return true;
                }
            }

            return false;
        }

        public static Enums.PostType? GetPostType(int? postTypeId)
        {
            if (postTypeId.HasValue)
            {
                foreach (Enums.PostType postType in Enum.GetValues(typeof(Enums.PostType)))
                {

                    if ((int)postType == postTypeId)
                    {
                        return postType;
                    }
                }
            }
            
            return null;
        }

        public static Enums.Status? GetStatus(int? selectedStatus)
        {
            if (selectedStatus.HasValue)
            {
                foreach (Enums.Status status in Enum.GetValues(typeof(Enums.Status)))
                {

                    if ((int)status == selectedStatus)
                    {
                        return status;
                    }
                }
            }

            return null;
        }

        public static string GetStatusName(Enums.Status status)
        {
            string str = "";

            switch (status)
            {
                case Enums.Status.Active:
                    str = "Đã kích hoạt";
                    break;
                case Enums.Status.InActive:
                    str = "Chưa kích hoạt";
                    break;
                default:
                    str = "";
                    break;
            }

            return str;
        }
    }
}
