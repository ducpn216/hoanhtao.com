using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.Models
{
    public class HomeModel : BaseModel
    {
        public HomeModel()
        {
            ServerList = new List<Server>();
        }

        public List<Server> ServerList { get; set; }
        public List<Banner> BannerList { get; set; }
        public List<Post> NewsList { get; set; }
        public List<Post> EventList { get; set; }

        public List<Post> GioiThieuList { get; set; }
        public List<Post> TanThuList { get; set; }
        public List<Post> FaqList { get; set; }
        public int CurrentKNB { get; set; }
    }
}