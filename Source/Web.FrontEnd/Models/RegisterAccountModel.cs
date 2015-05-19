using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.Models
{
    public class RegisterAccountModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password1 { get; set; }

        [Required]
        public string ConfirmPassword1 { get; set; }

        [Required]
        public string Password2 { get; set; }

        [Required]
        public string ConfirmPassword2 { get; set; }

        [Required]
        public string Email { get; set; }
        
        public int QuestionId { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string IdentityCard { get; set; }

        [Required]
        public string IP { get; set; }
    }
}