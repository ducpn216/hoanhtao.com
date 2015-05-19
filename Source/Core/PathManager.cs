using Core.Constants;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Domain
{
    public class PathManager
    {
        public static string GetNewsImagePath(int newsId, object imageName)
        {
            if (imageName == null || imageName.ToString() == "")
            {
                return "";
            }

            return String.Format("/{0}/{1}/{2}", ConfigConstants.NEWS_FOLDER_PATH.Trim('/'), newsId, imageName);
        }

        public static string GetBannerImagePath(int bannerId, object imageName)
        {
            if (imageName == null || imageName.ToString() == "")
            {
                return "";
            }

            return String.Format("/{0}/{1}/{2}", ConfigConstants.BANNER_FOLDER_PATH.Trim('/'), bannerId, imageName);
        }

        public static string GetSupportImagePath(int bannerId, object imageName = null)
        {
            if (imageName == null || imageName.ToString() == "")
            {
                return String.Format("/{0}/{1}/", ConfigConstants.SUPPORT_FOLDER_PATH.Trim('/'), bannerId);

            }

            return String.Format("/{0}/{1}/{2}", ConfigConstants.SUPPORT_FOLDER_PATH.Trim('/'), bannerId, imageName);
        }

        public static string GetSupportDetailImagePath(int id, object imageName = null)
        {
            if (imageName == null || imageName.ToString() == "")
            {
                return String.Format("/{0}/{1}/", ConfigConstants.SUPPORT_DETAIL_FOLDER_PATH.Trim('/'), id);

            }

            return String.Format("/{0}/{1}/{2}", ConfigConstants.SUPPORT_DETAIL_FOLDER_PATH.Trim('/'), id, imageName);
        }

        public static string GetItemImagePath(int itemId, object imageName)
        {
            if (imageName == null || imageName.ToString() == "")
            {
                return "";
            }

            return String.Format("/{0}/{1}/{2}", ConfigConstants.ITEM_FOLDER_PATH.Trim('/'), itemId, imageName);
        }

        public static string ErrorFilePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/Errors/") + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
        }
    }
}
