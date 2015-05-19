using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Core
{
    public class FileLog
    {
        public static string EventName = "";
        public static string FullErrorPath;

        public static void Write(string functionFolder, string functionName, string ErrDesc, bool isNewLine = false)
        {
            if (Directory.Exists(FullErrorPath) == false)
            {
                Directory.CreateDirectory(FullErrorPath);
            }

            string fileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + ".txt";

            //if (Directory.Exists(FullErrorPath + "/" + EventName + "/") == false)
            //{
            //    Directory.CreateDirectory(FullErrorPath + "/" + EventName + "/");
            //}

            //if (Directory.Exists(FullErrorPath + "/" + EventName + "/" + functionFolder + "/") == false)
            //{
            //    Directory.CreateDirectory(FullErrorPath + "/" + EventName + "/" + functionFolder + "/");
            //}

            //string fullPath = FullErrorPath + "/" + EventName + "/" + functionFolder + "/" + fileName;

            if (Directory.Exists(FullErrorPath + "/" + functionFolder + "/") == false)
            {
                Directory.CreateDirectory(FullErrorPath + "/" + functionFolder + "/");
            }

            string fullPath = FullErrorPath  + "/" + functionFolder + "/" + fileName;

            using(StreamWriter writer = new StreamWriter(fullPath, true, Encoding.UTF8)) 
            {
                if (isNewLine)
                {
                    writer.Write(writer.NewLine);
                }

                writer.WriteLine(DateTime.Now.ToString() + ", " + functionName + ", " + ErrDesc);
            }
        }
    }
}
