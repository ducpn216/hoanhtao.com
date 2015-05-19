using Core.API.GameApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class RoleListPageModel
    {
        public RoleListPageModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
            RoleDataList = new List<RoleListResponse>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }

        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public List<RoleListResponse> RoleDataList { get; set; }
    }
}