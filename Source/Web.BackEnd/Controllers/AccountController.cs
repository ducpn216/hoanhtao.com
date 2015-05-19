using Core;
using Core.Providers;
using DAL.Reponses;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.BackEnd.App_Code;
using Web.BackEnd.App_Code.Attributes;
using Web.BackEnd.Models;

namespace Web.BackEnd.Controllers
{

    public class AccountController : Controller
    {
        InsideRepository insideRepository = new InsideRepository();

        public AccountController()
        {

        }

        #region Login

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                int userId = -1;
                int groupId = -1;

                int flag = insideRepository.LoginInside(loginModel.AccountName, loginModel.Password, CommonProvider.FunctionProvider.GetIp(),
                    ref userId, ref groupId);

                if (flag == 0)
                {
                    string userData = loginModel.AccountName + "&&&" + userId.ToString() + "&&&" + groupId;
                    CommonProvider.AuthenticationProvider.CreateAuthentication(loginModel.AccountName, DateTime.Now, DateTime.Now.AddMinutes(30),
                        true, userData);
                    return Redirect("/Post");
                }
                else
                {
                    ModelState.AddModelError("message", "Tài khoản không hợp lệ");
                    TempData["Message"] = Helper.GetErrorsOfModelState(ModelState);
                }
            }
            else
            {
                TempData["Message"] = Helper.GetErrorsOfModelState(ModelState);
            }

            return View(loginModel);
        }
        #endregion

        #region Change Password
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ChangePasswordModel loginModel = new ChangePasswordModel();
            return View(loginModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                ResultResponse response = insideRepository.ChangePassword(User.Identity.Name,
                    changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
                if (response.Flag == 0)
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Thay đổi mật khẩu thành công");
                }
                else
                {
                    ModelState.AddModelError("message", "Sai mật khẩu hiện tại");
                    TempData["Message"] = BackEndUtilities.GetErrorsOfModelState(ModelState);
                }
            }
            else
            {
                TempData["Message"] = BackEndUtilities.GetErrorsOfModelState(ModelState);
            }

            return View(changePasswordModel);
        }
        #endregion

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

    }
}