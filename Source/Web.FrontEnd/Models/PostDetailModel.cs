using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class PostDetailModel : InsideModel
    {
        public PostDetailModel()
            : base()
        {

        }

        public List<Post> OtherPostList { get; set; }
        public int PostId { get; set; }
        public int PostTypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}