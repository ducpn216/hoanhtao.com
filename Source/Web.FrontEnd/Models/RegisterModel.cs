using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models
{
    public class RegisterModel : InsideModel
    {
        public RegisterModel()
        {
            //QuestionList = new List<SelectListItem>();
        }

        [Display(Name = "Username"), Required(ErrorMessage = "Nhập username")]
        public string Username { get; set; }

        [DataType(DataType.Password), Display(Name = "Mật khẩu cấp"), Required(ErrorMessage = "Nhập mật khẩu"), ]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Xác nhận mật khẩu"), Required(ErrorMessage = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

        //[DataType(DataType.Password), Display(Name = "Mật khẩu cấp 2"), Required(ErrorMessage = "Nhập mật khẩu cấp 2")]
        //public string Password2 { get; set; }

        //[DataType(DataType.Password), Display(Name = "Xác nhận mật khẩu cấp 2"), Required(ErrorMessage = "Xác nhận mật khẩu cấp 2")]
        //public string ConfirmPassword2 { get; set; }

        //[Display(Name = "Email"), Required(ErrorMessage = "Nhập email"), EmailAddress(ErrorMessage = "Email không hợp lệ")]
        //public string Email { get; set; }

        //[Display(Name = "Câu trả lời"), Required(ErrorMessage = "Nhập câu trả lời")]
        //public string Answer { get; set; }

        //[Display(Name = "Số điện thoại"), Required(ErrorMessage = "Nhập số điện thoại")]
        //public string PhoneNumber { get; set; }

        //[Display(Name = "CMND")]
        //public string IdentityCard { get; set; }

        public string IP { get; set; }
        public string Captcha { get; set; }
        public bool IsSuccess { get; set; }

        //public List<SelectListItem> QuestionList { get; set; }

        // [Display(Name = "Câu hỏi bí mật")]
        //public int QuestionId { get; set; }
    }
}