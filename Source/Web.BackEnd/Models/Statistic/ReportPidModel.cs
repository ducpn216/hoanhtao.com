using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models.Statistic
{
    public class ReportPidModel
    {
        public ReportPidModel()
        {
            PidList = new List<SelectListItem>();
            From = DateTime.Now.ToString("dd/MM/yyyy");
            To = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public List<SelectListItem> PidList { get; set; }
        public string SelectedPid { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public DataSet DataList { get; set; }
    }
}