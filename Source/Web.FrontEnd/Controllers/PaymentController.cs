using Core;
using Core.API;
using Core.API.GameApiRequests;
using Core.API.GameApiResponses;
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
using Web.FrontEnd.App_Code.Attributes;
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

        #region Index
        //[HttpGet]
        //[CustomAuthorize(LinkManager.PAYMENT)]
        //public ActionResult Index()
        //{
        //    PaymentModel paymentModel = new PaymentModel()
        //    {
        //        CardList = GetCardList(null),
        //        ServerList = FrontEndUtils.GetServerList(null),
        //        Message = ""
        //    };

        //    return View(paymentModel);
        //}

        //[HttpPost]
        //[CustomAuthorize(LinkManager.PAYMENT)]
        //public async Task<ActionResult> Index(PaymentModel paymentModel)
        //{
        //    paymentModel.Message = "";
        //    string message = ValidatePayment(paymentModel);

        //    if (string.IsNullOrEmpty(message))
        //    {
        //        string username = AuthHelper.GetUsername();
        //        //if (!string.IsNullOrEmpty(SessionManager.FacebookAccessToken) ||
        //        //    !string.IsNullOrEmpty(SessionManager.GoogleAccessToken))
        //        //{
        //        //    username = AuthHelper.GetUserId().ToString();
        //        //}

        //        PaymentResponse response = await paymentProvider.Charge(new PaymentRequest()
        //        {
        //            Serial = paymentModel.Serial,
        //            Pin = paymentModel.Pin,
        //            Provider = paymentModel.SelectedCardType,
        //            ProductId = "WG2"
        //        });

        //        //FileLog.Write("Payment", "Charge", "Pass pay");

        //        if (response != null)
        //        {
        //            PaymentRepository paymentRepository = new PaymentRepository();
        //            CurrencyRepository currencyRepository = new CurrencyRepository();

        //            string data = paymentModel.SelectedCardType + "_" + paymentModel.Serial + "_" + paymentModel.Pin;

        //            if (response.status == 1)
        //            {
        //                GameAPI gameAPI = new GameAPI();
        //                long orderId = paymentRepository.GetOrderId();
        //                int knb = 0;

        //                //FileLog.Write("Payment", "OrderId", orderId.ToString());

        //                paymentModel.Message = "Nạp thẻ thành công";
        //                paymentModel.IsValid = true;

        //                Currency currency = currencyRepository.GetByMoney(Convert.ToInt64(response.amount));
        //                if (currency != null)
        //                {
        //                    knb = currency.KNB;
        //                }

        //                //FileLog.Write("Payment", "knb", knb.ToString());

        //                int result = await gameAPI.Charge(new ChargeRequest()
        //                {
        //                    AccountId = AuthHelper.GetUserId(),
        //                    ServerId = paymentModel.SelectedServerId,
        //                    TransId = orderId.ToString(),
        //                    GamePoint = knb
        //                });

        //                if (result == 1)
        //                {
        //                    paymentRepository.TransferCard(username, response.trans_id, Convert.ToInt32(response.amount), data, orderId, response.status);
        //                }
        //                else
        //                {
        //                    paymentRepository.TransferCard(username, response.trans_id, Convert.ToInt32(response.amount), data, orderId, 1000);
        //                }

        //                //SessionManager.PaymentTime = null;
        //            }
        //            else
        //            {
        //                //SessionManager.PaymentTime = DateTime.Now.AddMinutes(5);

        //                //paymentModel.Message = paymentProvider.GetErrorMessage(response.retCode);
        //                paymentRepository.TransferCard(username, response.trans_id, 0, data, -1, response.status);
        //                paymentModel.Message = response.msg;
        //                //paymentModel.Message = paymentProvider.GetErrorMessage(response.status);
        //            }
        //        }
        //        else
        //        {
        //            paymentModel.Message = "Có lỗi trong quá trình xử lý";

        //            SessionManager.PaymentTime = DateTime.Now.AddMinutes(5);
        //        }
        //    }
        //    else
        //    {
        //        paymentModel.Message = message;
        //    }

        //    paymentModel.CardList = GetCardList(paymentModel.SelectedCardType);
        //    paymentModel.ServerList = FrontEndUtils.GetServerList(paymentModel.SelectedServerId);

        //    return View(paymentModel);
        //}
        #endregion
       
        #region Transfer Card test
        [HttpGet]
        [CustomAuthorize(LinkManager.PAYMENT)]
        public ActionResult TransferCard()
        {
            PaymentModel paymentModel = new PaymentModel()
            {
                CardList = GetCardList(null),
                //ServerList = FrontEndUtils.GetServerList(null),
                Message = ""
            };

            return View(paymentModel);
        }

        [HttpPost]
        [CustomAuthorize(LinkManager.PAYMENT)]
        public async Task<ActionResult> TransferCard(PaymentModel model)
        {
            model.Message = "";

            if (ModelState.IsValid)
            {
                string username = AuthHelper.GetUsername();
                //if (!string.IsNullOrEmpty(SessionManager.FacebookAccessToken) ||
                //    !string.IsNullOrEmpty(SessionManager.GoogleAccessToken))
                //{
                //    username = AuthHelper.GetUserId().ToString();
                //}

                PaymentResponse response = await paymentProvider.Charge(new PaymentRequest()
                {
                    Serial = model.Serial,
                    Pin = model.Pin,
                    Provider = model.SelectedCardType,
                    ProductId = "WG2"
                });

                if (response != null)
                {
                    PaymentRepository paymentRepository = new PaymentRepository();
                    CurrencyRepository currencyRepository = new CurrencyRepository();

                    string data = model.SelectedCardType + "_" + model.Serial + "_" + model.Pin;
                    int amount = 0;
                    int knb = 0;

                    if (response.status == 1)
                    {
                        Currency currency = currencyRepository.GetByMoney(Convert.ToInt64(response.amount));
                        amount = Convert.ToInt32(response.amount);

                        if (currency != null)
                        {
                            knb = currency.KNB;
                        }

                        model.IsValid = true;
                        model.Message = "Nạp thẻ thành công";

                        SessionManager.PaymentTime = null;
                    }
                    else
                    {
                        //model.Message = Factory.PaymentProvider.GetErrorMessage(response.status);
                        model.Message = response.msg;

                        SessionManager.PaymentTime = DateTime.Now.AddMinutes(5);
                    }

                    paymentRepository.TransferCardTest(response.trans_id, AuthHelper.GetUserId(), amount, knb, data,
                        model.Pin, model.Serial, response.status, CommonProvider.FunctionProvider.GetIp(), model.Message);

                    FileLog.Write("Payment", "Payment",
                            string.Format("{0} | {1} | {2} | {3} | {4} | {5}",
                            model.Message, AuthHelper.GetUsername(), 0, model.SelectedCardType, model.Serial, model.Pin));
                }
                else
                {
                    model.Message = "Có lỗi trong quá trình xử lý";

                    //SessionManager.PaymentTime = DateTime.Now.AddMinutes(5);

                    FileLog.Write("Payment", "Payment",
                            string.Format("NULL {0} | {1} | {2} | {3} | {4} | {5}",
                            model.Message, AuthHelper.GetUsername(), 0, model.SelectedCardType, model.Serial, model.Pin));

                    Factory.PaymentRepository.TransferCardTest("", AuthHelper.GetUserId(), 0, 0, "",
                        model.Pin, model.Serial, 5000, CommonProvider.FunctionProvider.GetIp(), "Pending từ đối tác");
                }
            }
            else
            {
                model.Message = Helper.GetErrorsOfModelState(ModelState);
            }

            model.CardList = GetCardList(model.SelectedCardType);

            return View(model);
        }

        #endregion

        [HttpGet]
        [CustomAuthorize(LinkManager.TRANSFER_GOLD)]
        public ActionResult TransferGold()
        {
            TransferModel model = new TransferModel()
            {
                CurrentKNB = Factory.UserRepository.GetGold(AuthHelper.GetUserId()),
                ServerList = FrontEndUtils.GetServerList(null),
                KNBList = FrontEndUtils.GetKNBList()
            };

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(LinkManager.TRANSFER_GOLD)]
        public async Task<ActionResult> TransferGold(TransferModel model)
        {
            if (ModelState.IsValid)
            {
                if (CheckKnbValid(model.SelectedKNB))
                {
                    int currentGold = Factory.UserRepository.GetGold(AuthHelper.GetUserId());
                    if (model.SelectedKNB > currentGold)
                    {
                        model.Message = "KNB không đủ để chuyển vào game";
                    }
                    else
                    {
                        List<RoleListResponse> roleList = await Factory.GameAPI.GetRoleList(new RoleListRequest()
                        {
                            AccountId = AuthHelper.GetUserId(),
                            ServerId = model.SelectedServerId,
                            Page = 1,
                            PageSize = 3
                        });

                        if (roleList.Count > 0)
                        {
                            long orderId = Factory.PaymentRepository.GetOrderIdTest();

                            int result = await Factory.GameAPI.Charge(new ChargeRequest()
                            {
                                AccountId = AuthHelper.GetUserId(),
                                ServerId = model.SelectedServerId,
                                TransId = orderId.ToString(),
                                GamePoint = model.SelectedKNB
                            });

                            if (result == 1)
                            {
                                model.IsValid = true;
                                model.Message = "Chuyển kim nguyên bảo vào game thành công";
                            }
                            else
                            {
                                model.Message = "Chuyển kim nguyên bảo vào game thất bại";
                            }

                            Factory.PaymentRepository.TransferGoldToGame(AuthHelper.GetUserId(), model.SelectedServerId, orderId,
                                model.SelectedKNB, result, CommonProvider.FunctionProvider.GetIp());
                        }
                        else
                        {
                            model.Message = "Chưa tạo nhân vật";
                        }
                    }
                }
                else
                {
                    model.Message = "Kim nguyên bảo không hợp lệ";
                }
            }
            else
            {
                model.IsValid = false;
                model.Message = Helper.GetErrorsOfModelState(ModelState);
            }

            model.CurrentKNB = Factory.UserRepository.GetGold(AuthHelper.GetUserId());
            model.ServerList = FrontEndUtils.GetServerList(model.SelectedServerId);
            model.KNBList = FrontEndUtils.GetKNBList();

            return View(model);
        }

        bool CheckKnbValid(int knb)
        {
            if (knb == 100 || knb == 200 || knb == 300 || knb == 500 || knb == 1000 || knb == 2000 || knb == 5000)
            {
                return true;
            }

            return false;
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