using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public class GlobalConstants
    {
        public static int GameId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["GameId"]);
            }
        }

        public const string CULTURE_VIETNAM = "vi-VN";
    }
}
