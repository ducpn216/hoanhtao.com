using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class ServerModel
    {
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public short Status { get; set; }
        public bool IsReal { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }
    }

    public class ServerListModel
    {
        public ServerListModel()
        {
            ServerList = new List<ServerModel>();
            StatusList = new List<SelectListItem>();
        }

        public List<ServerModel> ServerList { get; set; }

        public List<SelectListItem> StatusList { get; set; }
        public short SelectedStatus { get; set; }
    }

    public class ServerFormModel
    {
        public ServerFormModel()
        {
            StatusList = new List<SelectListItem>();
            ServerModel = new ServerModel();
        }

        public ServerModel ServerModel { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public short SelectedStatus { get; set; }
    }
}