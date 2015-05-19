using Core;
using Core.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.MVC.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class IsNumericAttribute : ValidationAttribute
    {
        private string Value { get; set; }

        public IsNumericAttribute()
        {
            //this.Value = value;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), "^[0-9]*$"))
                {
                    return true;
                }
                //int number = 0;
                //if (int.TryParse(value.ToString(), out number))
                //{
                //    return true;
                //}
            }

            return false;
        }
    }
}