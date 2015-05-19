using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class OnlineNumberPageModel
    {
        public int OnlineNumber { get; set; }
        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; } 
    }
}