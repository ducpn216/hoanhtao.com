using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models
{
    public class SendMailModel : ModelAbstract
    {
        public SendMailModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        public string AccountName { get; set; }
        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }

        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public bool SendAll { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Gold { get; set; }
        public int BindGold { get; set; }
        public int Money { get; set; }
        public string ItemId1 { get; set; }
        public int NumberOfItem1 { get; set; }
        public string ItemId2 { get; set; }
        public int NumberOfItem2 { get; set; }
        public string ItemId3 { get; set; }
        public int NumberOfItem3 { get; set; }
        public string ItemId4 { get; set; }
        public int NumberOfItem4 { get; set; }
        public string ItemId5 { get; set; }
        public int NumberOfItem5 { get; set; }

    }
}