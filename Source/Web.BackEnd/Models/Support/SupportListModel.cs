using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models.Support
{
    public class SupportListModel
    {
        public SupportListModel()
        {

        }

        public List<SelectListItem> StatusList { get; set; }
        public int SelectedStatus { get; set; }

        public List<DAL.Models.Support> DataList { get; set; }
    }
}