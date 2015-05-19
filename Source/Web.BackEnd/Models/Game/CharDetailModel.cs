using Core.GameAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class CharDetailModel
    {
        public CharDetailModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
            //CharDetailResponse = new CharDetailResponse();
        }

        public string AccountName { get; set; }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }

        public CharDetailResponse CharDetailResponse { get; set; }
        public int WebKNB { get; set; }
        public int GameKNB { get; set; }
    }
}