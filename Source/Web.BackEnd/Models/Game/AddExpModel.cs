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
    public class AddExpModel : ModelAbstract
    {
        public AddExpModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public int SelectedServerId { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public int SelectedRoleId { get; set; }
        
        [Required(ErrorMessage = "Nhập tài khoản")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Nhập kinh nghiệm")]
        public int Exp { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }
    }
}