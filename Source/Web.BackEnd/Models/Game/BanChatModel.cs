using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models
{
    public class BanChatModel : ModelAbstract
    {
        public BanChatModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }

        public string AccountName { get; set; }
        public int Time { get; set; }
    }
}