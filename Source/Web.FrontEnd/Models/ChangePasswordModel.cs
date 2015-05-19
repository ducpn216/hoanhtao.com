using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.FrontEnd.Models
{
    public class ChangePasswordModel
    {
        [Display(Name = "Username"), Required(ErrorMessage = "Nhập username")]
        public string Username { get; set; }

        [DataType(DataType.Password), Display(Name = "Mật khẩu cấp 1 mới"), Required(ErrorMessage = "Nhập mật khẩu cấp 1 mới"),]
        public string NewPassword1 { get; set; }

        [DataType(DataType.Password), Display(Name = "Xác nhận mật khẩu cấp 1"), Required(ErrorMessage = "Xác nhận mật khẩu cấp 1")]
        public string ConfirmNewPassword1 { get; set; }

        [DataType(DataType.Password), Display(Name = "Mật khẩu cấp 2"), Required(ErrorMessage = "Nhập mật khẩu cấp 2")]
        public string Password2 { get; set; }
    }
}