using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class CCUModel
    {
        public CCUModel()
        {
            ServerList = new List<SelectListItem>();
            Date = DateTime.Now;
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }
        public int CCU { get; set; }
    }

    public class NRUModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int NRU {get;set;}
    }
}