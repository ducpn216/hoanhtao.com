using AutoMapper;
using Core;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;
using Web.BackEnd.App_Code.Attributes;
using Web.BackEnd.Models;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{
    [CustomAuthorize(ForbiddenGroupId = new[] { 2, 3 })]
    public class CurrencyController : BackEndController
    {
        CurrencyRepository currencyRepository = new CurrencyRepository();

        public CurrencyController()
        {

        }

        public ActionResult Index(int? parentId)
        {
            parentId = parentId.HasValue ? parentId.Value : 0;

            CurrencyListModel CurrencyListModel = new CurrencyListModel()
            {
                CurrencyList = currencyRepository.GetList()
            };

            return View(CurrencyListModel);
        }

        [HttpPost]
        public ActionResult UpdateOrder(CurrencyListModel currencyListModel)
        {
            if (currencyListModel != null)
            {
                List<int> idList = new List<int>();
                List<int> orderList = new List<int>();

                foreach (var currency in currencyListModel.CurrencyList)
                {
                    idList.Add(currency.Id);
                    orderList.Add(currency.KNB);
                }
                FileLog.Write("Currency", "",
                    currencyListModel.CurrencyList.Count.ToString() + "&" + idList.Count.ToString() + "&" + orderList.Count.ToString());
                if (currencyRepository.UpdateOrder(idList, orderList))
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thành công", Enums.ErrorCode.Success);
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thất bại", Enums.ErrorCode.Error);
                }
            }

            return RedirectToAction("Index", "Currency");
        }
    }
}