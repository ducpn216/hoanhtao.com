using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.Models
{
    public class LauncherModel
    {
        public LauncherModel()
        {
            BannerList = new List<Banner>();
            NewsList = new List<Post>();
        }

        public List<Banner> BannerList { get; set; }
        public List<Post> NewsList { get; set; }
        public List<Post> EventList { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Captcha { get; set; }
        public string Message { get; set; }
    }
}