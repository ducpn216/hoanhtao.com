using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class PostListModel
    {
        public PostListModel()
        {
            PostList = new List<PostModel>();
            CategoryList = new List<SelectListItem>();
            PostTypeList = new List<SelectListItem>();
        }

        public List<PostModel> PostList { get; set; }

        public List<SelectListItem> PostTypeList { get; set; }
        public int SelectedPostTypeId { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public int SelectedCategoryId { get; set; }
    }

    public class PostFormModel
    {
        public PostFormModel()
        {
            CategoryList = new List<SelectListItem>();
            PostTypeList = new List<SelectListItem>();
            PostModel = new PostModel();
        }

        public PostModel PostModel { get; set; }

        public List<SelectListItem> PostTypeList { get; set; }
        public int SelectedPostTypeId { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public int SelectedCategoryId { get; set; }

        public string Image { get; set; }
        public string Captcha { get; set; }
    }

    public class PostModel
    {
        public int PostId { get; set; }
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string PostTypeId { get; set; }
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}