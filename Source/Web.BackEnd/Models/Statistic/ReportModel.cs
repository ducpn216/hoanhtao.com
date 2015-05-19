using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models.Statistic
{
    public class ReportModel
    {
        public ReportModel()
        {
            ServerList = new List<SelectListItem>();
            From = DateTime.Now.ToString("dd/MM/yyyy");
            To = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public DataSet DataList { get; set; }
    }
}