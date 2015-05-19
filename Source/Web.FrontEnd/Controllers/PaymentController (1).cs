using Core;
using Core.API;
using Core.GameAPI.Requests;
using Core.Providers;
using Core.Providers.Payment;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models;

namespace Web.FrontEnd.Controllers
{
    public class PaymentController : Controller
    {
        PaymentProvider paymentProvider = new PaymentProvider();

        public PaymentController()
        {

        }

        string ValidatePayment(PaymentModel paymentModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(paymentModel.SelectedCardType))
            {
                message += "Chọn máy chủ<br/>";
            }

            if (string.IsNullOrWhiteSpace(paymentModel.SelectedCardType))
            {
                message += "Chọn loại thẻ<br/>";
            }

            if (string.IsNullOrWhiteSpace(paymentModel.Serial))
            {
                message = "Nhập serial<br/>";
            }

            if (string.IsNullOrWhiteSpace(paymentModel.Pin))
            {
                message += "Nhập serial<br/>";
            }

            if (string.IsNullOrWhiteSpace(paymentModel.Captcha))
            {
                message += "Nhập mã xác nhận<br/>";
            }
            else if (paymentModel.Captcha != SessionManager.Captcha)
            {
                message += "Mã xác nhận không hợp lệ<br/>";
            }

            return message;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (await AuthHelper.CheckLogin())
            {
                //if (AuthHelper.CheckAdmin(User.Identity.Name))
                //{
                    
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                PaymentModel paymentModel = new PaymentModel()
                {
                    CardList = GetCardList(null),
                    ServerList = FrontEndUtils.GetServerList(null),
                    Message = ""
                };

                return View(paymentModel);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/NapThe/" });
            }

        }

        [HttpPost]
        public async Task<ActionResult> Index(PaymentModel paymentModel)
        {
            paymentModel.Message = "";
            string message = ValidatePayment(paymentModel);

            if (string.IsNullOrEmpty(message))
            {
                string username = AuthHelper.GetUsername();
                if (!string.IsNullOrEmpty(SessionManager.FacebookAccessToken) ||
                    !string.IsNullOrEmpty(SessionManager.GoogleAccessToken))
                {
                    username = AuthHelper.GetUserId().ToString();
                }

                PaymentResponse response = await paymentProvider.Charge(new PaymentRequest()
                {
                    Serial = paymentModel.Serial,
                    Pin = paymentModel.Pin,
                    Provider = paymentModel.SelectedCardType,
                    ProductId = "WG2"
                });

                FileLog.Write("Payment", "Charge", "Pass pay");

                if (response != null)
                {
                    PaymentRepository paymentRepository = new PaymentRepository();
                    CurrencyRepository currencyRepository = new CurrencyRepository();

                    string data = paymentModel.SelectedCardType + "_" + paymentModel.Serial + "_" + paymentModel.Pin;

                    if (response.status == 1)
                    {
                        GameAPI gameAPI = new GameAPI();
                        long orderId = paymentRepository.GetOrderId();
                        int knb = 0;

                        FileLog.Write("Payment", "OrderId", orderId.ToString());

                        paymentModel.Message = "Nạp thẻ thành công";
                        paymentModel.IsSuccess = true;

                        Currency currency = currencyRepository.GetByMoney(Convert.ToInt64(response.amount));
                        if (currency != null)
                        {
                            knb = currency.KNB;
                        }

                        FileLog.Write("Payment", "knb", knb.ToString());

                        int result = await gameAPI.Charge(new ChargeRequest()
                        {
                            AccountId = AuthHelper.GetUserId(),
                            ServerId = paymentModel.SelectedServerId,
                            TransId = orderId.ToString(),
                            GamePoint = knb
                        });

                        if (result == 1)
                        {
                            paymentRepository.Insert(username, response.trans_id, Convert.ToInt32(response.amount), data, orderId, response.status);
                        }
                        else
                        {
                            paymentRepository.Insert(username, response.trans_id, Convert.ToInt32(response.amount), data, orderId, 1000);
                        }
                    }
                    else
                    {
                        //paymentModel.Message = paymentProvider.GetErrorMessage(response.retCode);
                        paymentRepository.Insert(username, response.trans_id, 0, data, -1, response.status);
                        paymentModel.Message = paymentProvider.GetErrorMessage(response.status);
                    }
                }
                else
                {
                    paymentModel.Message = "Có lỗi trong quá trình xử lý";
                }
            }
            else
            {
                paymentModel.Message = message;
            }

            paymentModel.CardList = GetCardList(paymentModel.SelectedCardType);
            paymentModel.ServerList = FrontEndUtils.GetServerList(paymentModel.SelectedServerId);

            return View(paymentModel);
        }

        List<SelectListItem> GetCardList(string cardType = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Dictionary<string, string> cardList = paymentProvider.GetCardList();

            if (cardList != null)
            {
                foreach (var card in cardList)
                {
                    bool selected = (cardType != null && card.Key == cardType);
                    list.Add(new SelectListItem() { Text = card.Value, Value = card.Key, Selected = selected });
                }
            }

            list.Insert(0, new SelectListItem() { Text = "Chọn thẻ", Value = "" });
            return list;
        }

    }
}