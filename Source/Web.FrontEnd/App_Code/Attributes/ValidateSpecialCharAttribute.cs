using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.App_Code.Attributes
{
    public class ValidateSpecialCharAttribute : ValidationAttribute
    {
        public string Value { get; set; }

        public ValidateSpecialCharAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            return Helper.ContainsSpecialChar(value.ToString());
        }
    }
}