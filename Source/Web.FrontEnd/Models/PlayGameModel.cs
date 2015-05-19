using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.Models
{
    public class PlayGameModel
    {
        public PlayGameModel()
        {
            BannerList = new List<Banner>();
            PostList = new List<Post>();
            ServerList = new List<Server>();
        }

        public List<Banner> BannerList { get; set; }
        public List<Post> PostList { get; set; }
        public List<Server> ServerList { get; set; }
        public List<Server> NewServerList { get; set; }
    }
}