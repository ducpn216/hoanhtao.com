using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class PaymentModel
    {
        public PaymentModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }

        public int KNB { get; set; }
        public string AccountName { get; set; }
        public string Captcha { get; set; }
    }


}