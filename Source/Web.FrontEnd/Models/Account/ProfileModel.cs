﻿using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models.Account
{
    public class ProfileModel : InsideModel
    {
        [Display(Name = "Email"), Required(ErrorMessage = "Nhập email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không hợp lệ")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Phone"), Required(ErrorMessage = "Nhập số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}