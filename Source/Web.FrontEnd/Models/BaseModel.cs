using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.FrontEnd.App_Code;

namespace Web.FrontEnd.Models
{
    public class BaseModel
    {
        public string DisplayName
        {
            get
            {
                return AuthHelper.GetUsername();
            }
        }
    }
}