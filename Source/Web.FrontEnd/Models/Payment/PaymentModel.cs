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
    public class PaymentModel : InsideModel
    {
        public PaymentModel()
            : base()
        {
            CardList = new List<SelectListItem>();
            ServerList = new List<SelectListItem>();
        }

        public int CurrentKNB { get; set; }

        public List<SelectListItem> CardList { get; set; }
        public List<SelectListItem> ServerList { get; set; }

        [Required(ErrorMessage = "Chọn loại thẻ")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Loại thẻ không được chứa ký tự đặc biệt")]
        public string SelectedCardType { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Chọn server")]
        //public int SelectedServerId { get; set; }

        [Required(ErrorMessage = "Nhập mã serial")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Serial không được chứa ký tự đặc biệt")]
        public string Serial { get; set; }

        [Required(ErrorMessage = "Nhập mã pin")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Pin không được chứa ký tự đặc biệt")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }

        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}