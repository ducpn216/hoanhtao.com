using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.Models
{
    public class LauncherServerModel
    {
        public LauncherServerModel()
        {
            ServerList = new List<Server>();
        }

        public List<Server> ServerList { get; set; }
        public Server RecentServer { get; set; }
    }
}