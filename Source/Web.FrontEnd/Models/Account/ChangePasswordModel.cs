using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models.Account
{
    public class ChangePasswordModel : InsideModel
    {
        [DataType(DataType.Password), Display(Name = "Mật khẩu hiện tại"), Required(ErrorMessage = "Nhập mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password), Display(Name = "Mật khẩu mới"), Required(ErrorMessage = "Nhập mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Display(Name = "Xác nhận mật khẩu mới"), Required(ErrorMessage = "Xác nhận mật khẩu mới")]
        [System.Web.Mvc.CompareAttribute("NewPassword", ErrorMessage = "Xác nhận mật khẩu mới không hợp lệ")]
        public string ConfirmNewPassword { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}