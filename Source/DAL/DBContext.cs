using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DBContext
    {
        //public static DBDataContext Context
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Items["DBContext"] == null)
        //            System.Web.HttpContext.Current.Items["DBContext"] = new DBDataContext(ConfigurationManager.ConnectionStrings["WebSqlConnection"].ConnectionString);
        //        return (DBDataContext)System.Web.HttpContext.Current.Items["DBContext"];
        //    }
        //}

        private static DBDataContext context = null;

        public static DBDataContext Context
        {
            get
            {
                //if (System.Web.HttpContext.Current.Items["DBContext"] == null)
                //    System.Web.HttpContext.Current.Items["DBContext"] = new DBDataContext(ConfigurationManager.ConnectionStrings["WebSqlConnection"].ConnectionString);
                //return (DBDataContext)System.Web.HttpContext.Current.Items["DBContext"];

                if (context == null)
                    context = new DBDataContext(ConfigurationManager.ConnectionStrings["WebSqlConnection"].ConnectionString);
                return context;
            }
            set
            {
                context = value;
            }
        }
    }
}
    