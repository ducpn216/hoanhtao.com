using Core;
using Core.API;
using Core.GameAPI.Requests;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;
using Web.BackEnd.Models;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{
    public class PaymentController : BackEndController
    {
        public PaymentController()
        {

        }

        #region Payment
        [HttpGet]
        public ActionResult Index()
        {
            PaymentModel paymentModel = new PaymentModel()
            {
                ServerList = BackEndUtilities.GetServerList(null)
            };

            return View(paymentModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(PaymentModel paymentModel)
        {
            if (paymentModel != null)
            {
                string message = ValidatePayment(paymentModel);
                if (string.IsNullOrEmpty(message))
                {
                    UserRepository userRepository = new UserRepository();
                    PaymentRepository paymentRepository = new PaymentRepository();
                    GameAPI gameAPI = new GameAPI();

                    int accountId = userRepository.GetAccountId(paymentModel.AccountName);
                    if (accountId > 0)
                    {
                        long orderId = paymentRepository.GetOrderId();
                        int result = await gameAPI.Charge(new ChargeRequest()
                        {
                            AccountId = accountId,
                            GamePoint = paymentModel.KNB,
                            ServerId = paymentModel.SelectedServerId,
                            TransId = orderId.ToString()
                        });

                        string logs = "";
                        short status = 1;

                        if (result == 1)
                        {
                            logs = "Chuyển tiền thành công";
                            TempData["Message"] = BackEndUtilities.SetMessage("Chuyển tiền thành công", Core.Enums.ErrorCode.Success);
                        }
                        else
                        {
                            logs = "Chuyển tiền thất bại";
                            status = 0;

                            TempData["Message"] = BackEndUtilities.SetMessage("Chuyển tiền thất bại", Core.Enums.ErrorCode.Error);
                        }

                        logs += " | Account:" + paymentModel.AccountName +
                                " | ServerId :" + paymentModel.SelectedServerId +
                                " | KNB :" + paymentModel.KNB;

                        BackEndUtilities.WriteLog((int)Enums.BackEndFunction.AddMoney, logs, status);
                    }
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage(message, Core.Enums.ErrorCode.Error);
                }
            }

            return View(paymentModel);
        }

        string ValidatePayment(PaymentModel paymentModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(paymentModel.AccountName))
            {
                message = "Nhập tài khoản<br/>";
            }

            if (paymentModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            if (paymentModel.SelectedRoleId <= 0)
            {
                message += "Chọn nhân vật<br/>";
            }

            if (paymentModel.KNB <= 0)
            {
                message += "KNB không hợp lệ<br/>";
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

        #endregion

        public ActionResult Currency()
        {

            return View();
        }
    }
}