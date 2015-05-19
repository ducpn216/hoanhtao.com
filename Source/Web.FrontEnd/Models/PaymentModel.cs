using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class PaymentModel : InsideModel
    {
        public PaymentModel()
            : base()
        {
            CardList = new List<SelectListItem>();
            ServerList = new List<SelectListItem>();
            CharList = new List<SelectListItem>();
        }

        public List<SelectListItem> CardList { get; set; }
        public string SelectedCardType { get; set; }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> CharList { get; set; }
        public int SelectedCharId { get; set; }

        public string Serial { get; set; }
        public string Pin { get; set; }
        public string Captcha { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}