using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class PostListModel : InsideModel
    {
        public PostListModel()
            : base()
        {
            PostList = new List<Post>();
        }

        public int PostTypeId { get; set; }
        public List<Post> PostList { get; set; }
    }
}