using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVC.Attributes
{
    public class ValidateCaptchaAttribute : ValidationAttribute
    {
        public string Captcha { get; set; }

        public ValidateCaptchaAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            bool flag = false;

            if (value != null && value == this.Captcha)
            {
                flag = true;
            }

            return flag;
        }
    }
}
