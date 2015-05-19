using Core.Constants;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Core
{
    public class FilePathManager
    {
        public static string GetAvatarFilePath(int postId, string fileName, string folderPath)
        {
            return String.Format("{0}/{1}/{2}", folderPath, postId, fileName);
        }

        public static string GetPostImagePath(int postId, string fileName)
        {
            return String.Format("{0}/{1}/{2}", FolderPathConstansts.PostFolderPath, postId, fileName);
        }

        public static string GetBannerImagePath(int bannerId, string fileName)
        {
            return String.Format("{0}/{1}/{2}", FolderPathConstansts.BannerFolderPath, bannerId, fileName);
        }

        public static string GetSupportDetailImagePath(int supportDetailId, string fileName)
        {
            return String.Format("{0}/{1}/{2}", FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, supportDetailId, fileName);

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
