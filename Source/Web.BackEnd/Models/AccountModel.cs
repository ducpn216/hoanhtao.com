using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.BackEnd.Models
{
    public class LoginModel
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage="Nhập tài khoản")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ChangePasswordModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu hiện tại")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu mới")]
        [DataType(DataType.Password)]
        public string RetypeNewPassword { get; set; }
    }
}