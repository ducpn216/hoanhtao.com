using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models
{
    public class JumpMapModel : ModelAbstract
    {
        public JumpMapModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }

        public string AccountName { get; set; }
        public string MapId { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
    }
}