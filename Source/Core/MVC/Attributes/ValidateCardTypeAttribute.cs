using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVC.Attributes
{
    public class ValidateCardTypeAttribute : ValidationAttribute
    {
        public ValidateCardTypeAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            bool flag = false;
            
            if (value != null)
            {
                switch (value.ToString())
                {
                    case "MOBI":
                        flag = true;
                        break;
                    case "VT":
                        flag = true;
                        break;
                    case "VINA":
                        flag = true;
                        break;
                    case "FPT":
                        flag = true;
                        break;
                    case "VTC":
                        flag = true;
                        break;
                    default:
                        break;
                }
            }

            return flag;
        }
    }
}
