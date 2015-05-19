using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models
{
    public class KickOfflineModel : ModelAbstract
    {
        public KickOfflineModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        public string AccountName { get; set; }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }
        
        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; } 

    }
}