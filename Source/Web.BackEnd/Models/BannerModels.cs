using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class BannerListModel
    {
        public BannerListModel()
        {
            BannerList = new List<BannerModel>();
            StatusList = new List<SelectListItem>();
        }

        public List<BannerModel> BannerList { get; set; }

        public List<SelectListItem> StatusList { get; set; }
        public int SelectedStatus { get; set; }
    }

    public class BannerFormModel
    {
        public BannerFormModel()
        {
            BannerModel = new BannerModel();
            StatusList = new List<SelectListItem>();
        }

        public BannerModel BannerModel { get; set; }

        public List<SelectListItem> StatusList { get; set; }
        public short SelectedStatus { get; set; }
    }

    public class BannerModel
    {
        public int BannerId { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string BannerTypeId { get; set; }
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int GameId { get; set; }
    }
}