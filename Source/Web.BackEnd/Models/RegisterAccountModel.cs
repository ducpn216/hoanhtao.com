using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.BackEnd.Models
{
    public class RegisterAccountModel
    {
        public string Username { get;set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Email { get; set; }
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityCard { get; set; }
        public string IP { get; set; }
    }
}