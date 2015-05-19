using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.BackEnd.Models.Marketing
{
    public class PidListModel
    {
        public PidListModel()
        {
            PidList = new List<Pid>();
        }

        public List<Pid> PidList { get; set; }
    }
}