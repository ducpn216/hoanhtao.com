using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class TransferModel : InsideModel
    {
        public TransferModel()
            : base()
        {
            ServerList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public List<SelectListItem> KNBList { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Chọn server")]
        public int SelectedServerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Chọn số kim nguyên bảo")]
        public int SelectedKNB { get; set; }

        //[Required(ErrorMessage = "Nhập số kim nguyên bảo")]
        //[IsNumericAttribute(ErrorMessage = "Kim nguyên bảo không hợp lệ")]
        //[Range(1, int.MaxValue, ErrorMessage = "Kim nguyên bảo không hợp lệ")]
        //public int KNB { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }

        public int CurrentKNB { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}