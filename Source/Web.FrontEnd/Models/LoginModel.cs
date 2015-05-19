using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class LoginModel : InsideModel
    {
        [Display(Name = "Username"), Required(ErrorMessage = "Nhập username")]
        public string Username { get; set; }

        [DataType(DataType.Password), Display(Name = "Mật khẩu"), Required(ErrorMessage = "Nhập mật khẩu"), ]
        public string Password { get; set; }

        public string Captcha { get; set; }
        public bool ShowCaptcha { get; set; }
    }
}