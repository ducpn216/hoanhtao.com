using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.BackEnd.App_Code
{
    public abstract class ModelAbstract
    { 
        public string Captcha {get;set;}

        public bool IsCaptchaValid
        {
            get
            {
                if (SessionManager.Captcha == Captcha)
                {
                    return true; 
                }
                else{
                    return false;
                }
            }
        }

        public string CheckCaptcha()
        {
            string message = "";

            if (string.IsNullOrEmpty(Captcha))
            {
                message = "Nhập mã xác nhận<br/>";
            }
            else if (SessionManager.Captcha != Captcha)
            {
                message = "Mã xác nhận không hợp lệ";
            }

            return message;
        }
    }
}