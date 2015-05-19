using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.MVC.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public class ValidateContainSpecialCharAttribute : ValidationAttribute
    {
        private string Value { get; set; }

        public ValidateContainSpecialCharAttribute()
        {
            //this.Value = value;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                return !Helper.ContainsSpecialChar(value.ToString());
            }

            return true;
        }
    }
}