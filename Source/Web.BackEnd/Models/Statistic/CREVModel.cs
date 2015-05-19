using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models.Statistic
{
    public class CREVModel
    {
        public CREVModel()
        {
            GetDate = DateTime.Now;
        }

        public DateTime GetDate { get; set; }

        public string Date { get; set; }
        public int Total { get; set; }

        public int GeneralTotal { get; set; }
    }
}