using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models.Statistic
{
    public class ReportPaymentWebModel
    {
        public ReportPaymentWebModel()
        {
            ServerList = new List<SelectListItem>();
        }

        public int? UserId {get;set;}

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }
        
        public DataSet DataList { get; set; }
    }
}