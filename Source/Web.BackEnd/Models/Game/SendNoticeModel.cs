using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Models
{
    public class SendNoticeModel : ModelAbstract
    {
        public SendNoticeModel()
        {
            ServerList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        [Required(ErrorMessage = "Nhập thông báo")]
        public string Notice { get; set; }

        public string Link { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }
    }
}