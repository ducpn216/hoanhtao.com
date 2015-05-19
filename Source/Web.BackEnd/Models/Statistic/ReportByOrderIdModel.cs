using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models.Statistic
{
    public class ReportPaymentGameModel
    {
        public ReportPaymentGameModel()
        {
        }

        public int? UserId {get;set;}

        public DataSet DataList { get; set; }
    }
}