using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnectionConfig
    {
        public static String WebConnectionString
        {
            get;
            set;
        }

        public static String InsideConnectionString
        {
            get;
            set;
        }

        public static string ErrorFilePath
        {
            get;
            set;
        }
    }
}
