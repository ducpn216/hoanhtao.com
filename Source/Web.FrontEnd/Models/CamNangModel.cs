using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class CamNangModel
    {
        public List<Category> BiKip { get; set; }
        public List<Category> ThongTin { get; set; }
        public List<Category> VoLamSoNhap { get; set; }
        public List<Category> HuongDan { get; set; }
        public List<Category> GiaTangSucManh { get; set; }
        public List<Category> TinhNang { get; set; }
    }

    public class CamNangListModel : InsideModel
    {
        public CamNangListModel()
        {

        }

        public int PostId { get; set; }
        public int PostTypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Post> PostList { get; set; }
        public List<Post> OtherPostList { get; set; }
    }
}