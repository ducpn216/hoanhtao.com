using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models
{
    public class ModifyMallModel : ModelAbstract
    {
        public ModifyMallModel()
        {
            ServerList = new List<SelectListItem>();
            SellStatusList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> SellStatusList { get; set; }
        public int SelectedSellStatus { get; set; }

        public string ItemTypeId { get; set; }
        public string ItemId { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
    }
}