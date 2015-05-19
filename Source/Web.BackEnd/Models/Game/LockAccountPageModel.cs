using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class LockAccountPageModel
    {
        public LockAccountPageModel()
        {
            ServerList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
        }

        [Display(Name = "Tài khoản"), Required(ErrorMessage = "Nhập tài khoản")]
        public string AccountName { get; set; }

        [Display(Name = "Thời gian"), Required(ErrorMessage = "Nhập thời gian")]
        public int Time { get; set; }

        [Display(Name = "Lý do"), Required(ErrorMessage = "Nhập lý do")]
        public string Reason { get; set; }

        [Display(Name = "Server")]
        public List<SelectListItem> ServerList { get; set; }  
        public int SelectedServerId { get; set; }

        [Display(Name = "Nhân vật")]
        public List<SelectListItem> RoleList { get; set; }    
        public int SelectedRoleId { get; set; }
    }
}