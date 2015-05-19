using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Providers
{
    public class UploadProvider
    {
        public static Enums.UploadStatus Upload(int id, HttpPostedFileBase postedFile, string folderPath, string fileName)
        {
            Enums.UploadStatus status = Enums.UploadStatus.Fail;

            try
            {
                if (postedFile != null)
                {
                    HttpContext context = HttpContext.Current;
                    FileInfo fileInfo = new FileInfo(postedFile.FileName);

                    if (IsExtensionValid(fileInfo.Extension))
                    {
                        string fullFolderPath = string.Format("{0}\\{1}", context.Server.MapPath(folderPath), id);
                        FileLog.Write("Upload", "Upload", fullFolderPath);
                        if (!Directory.Exists(fullFolderPath))
                        {
                            Directory.CreateDirectory(fullFolderPath);
                        }

                        string path = string.Format("{0}\\{1}\\{2}",
                            context.Server.MapPath(folderPath), id, fileName);

                        FileLog.Write("Upload", "Upload", path);

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        postedFile.SaveAs(path);
                        status = Enums.UploadStatus.Success;
                    }
                    else
                    {
                        status = Enums.UploadStatus.InvalidExtension;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Upload", "Upload", ex.ToString());
            }

            return status;
        }

        public static bool IsExtensionValid(string extension)
        {
            string[] allowedFileExtensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            foreach (var ex in allowedFileExtensions)
            {
                if (extension == ex)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
