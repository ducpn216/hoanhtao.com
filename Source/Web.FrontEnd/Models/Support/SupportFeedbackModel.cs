using Core.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models.Abstracts;

namespace Web.FrontEnd.Models.Support
{
    public class SupportFeedbackModel : InsideModel
    {
        public SupportFeedbackModel()
        {
            ServerList = new List<SelectListItem>();
            SupportTypeList = new List<SelectListItem>();
        }

        public List<SelectListItem> ServerList { get; set; }
        public List<SelectListItem> SupportTypeList { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Chọn server")]
        public int SelectedServerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Chọn vấn đề hỗ trợ")]
        public int SelectedSupportTypeId { get; set; }

        [Required(ErrorMessage = "Nhập tiêu đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nhập nội dung")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Nhập mã xác nhận")]
        [ValidateContainSpecialCharAttribute(ErrorMessage = "Mã xác nhận không được chứa ký tự đặc biệt")]
        [System.Web.Mvc.CompareAttribute("CaptchaCode", ErrorMessage = "Mã xác nhận không hợp lệ")]
        public string Captcha { get; set; }

        public string CaptchaCode { get { return SessionManager.Captcha; } }

        public bool IsValid { get; set; }
        public string Message { get; set; }

        //public SupportFormModel SupportForm { get; set; }
    }
}