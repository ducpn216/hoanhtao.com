using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models.Marketing
{
    public class PidFormModel
    {
        public Guid Pid { get; set; }

        [Required(ErrorMessage = "Nhập tiêu đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nhập link")]
        public string Link { get; set; }

        //[Required(ErrorMessage = "Nhập mã xác nhận")]
        //[ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        //[System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        //public string Captcha { get; set; }

        //public string CaptchaCode { get { return SessionManager.Captcha; } }
        public bool IsValid { get; set; }
    }
}