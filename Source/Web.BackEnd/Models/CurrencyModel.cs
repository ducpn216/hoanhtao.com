using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class CurrencyListModel
    {
        public CurrencyListModel()
        {
            CurrencyList = new List<Currency>();
        }

        public List<Currency> CurrencyList { get; set; }
    }
}