using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models.Support
{
    public class SupportDetailModel : InsideModel
    {
        public SupportDetailModel()
        {

        }

        public DAL.Models.Support Support { get; set; }
        public List<DAL.Models.SupportDetail> SupportDetailList { get; set; }
        public bool IsResolved { get; set; }

        public SupportFormModel SupportForm { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}