//using BotDetect.Web;
using Core;
using Core.API;
using Core.Providers;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models;

namespace Web.FrontEnd.Controllers
{
    public class FrontEndApiController : Controller
    {
        UserRepository userRepository = new UserRepository();
        ServerRepository serverRepository = new ServerRepository();
        GameAPI gameAPI = new GameAPI();

        #region Register
        string IsRegisterValid(string username, string password, string retypePassword, string captcha, string inputCatpcha)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Nhập tài khoản<br/>";
            }
            else if (username.Length < 6 && username.Length > 12)
            {
                message = "Tài khoản bắt buộc từ 6-12 ký tự<br/>";
            }
            
            if (string.IsNullOrWhiteSpace(password))
            {
                message += "Nhập mật khẩu <br/>";
            }
            else if (password.Length < 6 && password.Length > 12)
            {
                message = "Mật khẩu phải từ 6-12 ký tự<br/>";
            }
            
            if (string.IsNullOrWhiteSpace(retypePassword))
            {
                message += "Xác nhận mật khẩu <br/>";
            }
            else if (retypePassword.Length < 6 && retypePassword.Length > 12)
            {
                message = "Xác nhận mật khẩu phải từ 6-12 ký tự<br/>";
            }
            else if (password != retypePassword)
            {
                message += "Xác nhận mật khẩu không đúng<br/>";
            }
            
            if (string.IsNullOrWhiteSpace(captcha))
            {
                message += "Nhập mã xác nhận<br/>";
            }
            else if (captcha != inputCatpcha)
            {
                message += "Mã xác nhận không hợp lệ";
            }

            return message;
        }

        public JsonResult Register(string username, string password, string retypePassword, string captcha, string pid = "")
        {
            ExecuteResultModel model = new ExecuteResultModel();
            model.Flag = 0;

            try
            {
                if (SessionManager.PID != null && !string.IsNullOrWhiteSpace(SessionManager.PID))
                {
                    pid = SessionManager.PID;
                }

                string checkValidMessage = IsRegisterValid(username, password, retypePassword, SessionManager.RegisterCaptcha, captcha);

                if (string.IsNullOrWhiteSpace(checkValidMessage))
                {
                    model = Register(username, password, pid);
                    if (model.Flag == 1)
                    {
                        SessionManager.PID = null;
                    }
                }
                else
                {
                    model.Message = checkValidMessage;
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            SessionManager.RegisterCaptcha = CommonProvider.FunctionProvider.GenerateString(3);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuickRegister(string username, string password, string retypePassword, string captcha)
        {
            //Thread.Sleep(10000);
            ExecuteResultModel model = new ExecuteResultModel();
            model.Flag = 0;

            try
            {
                string checkValidMessage = IsRegisterValid(username, password, retypePassword, SessionManager.QuickRegisterCaptcha, captcha);

                if (string.IsNullOrWhiteSpace(checkValidMessage))
                {
                    model = Register(username, password);
                }
                else
                {
                    model.Message = checkValidMessage;
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            SessionManager.QuickRegisterCaptcha = CommonProvider.FunctionProvider.GenerateString(3);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private ExecuteResultModel Register(string username, string password, string pid = "")
        {
            ExecuteResultModel model = new ExecuteResultModel();

            try
            {
                int flag = 0;
                string message = "";
                int userId = userRepository.Register(username, password, password,
                    "fast", 1, "fast", "fast", "fast", "fast", "fast", pid, ref flag, ref message);

                if (flag == 0)
                {
                    model.Flag = 1;

                    string userData = username + "&&&" + userId.ToString();
                    CommonProvider.AuthenticationProvider.CreateAuthentication(username, DateTime.Now, DateTime.Now.AddMinutes(30),
                        false, userData);

                    //Get recent server
                    DAL.Models.Server server = serverRepository.GetLatestServer();
                    if (server != null)
                    {
                        string gameUrl = gameAPI.GetGameUrl(userId, username, server.ServerId.ToString());

                        //update server & redirect to game
                        if (!string.IsNullOrWhiteSpace(gameUrl))
                        {
                            userRepository.UpdatePlayedServer(userId, server.ServerId);
                            model.Message = gameUrl;
                        }
                    }
                }
                else
                {
                    model.Message = message;
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return model;
        }

        #endregion

        #region Login
        string IsLoginValid(string username, string password, string captchaId, string instanceId, string userInput)
        {
            string message = "";
            if (string.IsNullOrEmpty(username))
            {
                message = "Nhập username";
            }
            //else if (username.Length < 6 && username.Length > 12)
            //{
            //    message = "Tài khoản bắt buộc từ 6-12 ký tự ";
            //}
            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Nhập mật khẩu";
            }
            //else if (password.Length < 6 && password.Length > 12)
            //{
            //    message = "Mật khẩu bắt buộc từ 6-12 ký tự ";
            //}

            //else if (string.IsNullOrWhiteSpace(userInput))
            //{
            //    message = "Nhập mã xác nhận";
            //}
            //else if (!CaptchaControl.AjaxValidate(captchaId, userInput, instanceId))
            //{
            //    message = "Mã xác nhận không hợp lệ";
            //}

            return message;
        }

        public JsonResult Login(string username, string password, string retypePassword, string captchaId, string instanceId, string userInput)
        {
            ExecuteResultModel model = new ExecuteResultModel();
            model.Flag = 0;

            try
            {
                string checkValidMessage = IsLoginValid(username, password, captchaId, instanceId, userInput);

                if (string.IsNullOrEmpty(checkValidMessage))
                {
                    int flag = 0;
                    int userId = userRepository.Login(username, password, ref flag);

                    if (userId > 0 && flag == 0)
                    {
                        model.Flag = 1;

                        //create authen
                        string userData = username + "&&&" + userId.ToString();
                        CommonProvider.AuthenticationProvider.CreateAuthentication(username, DateTime.Now, DateTime.Now.AddMinutes(30),
                            false, userData);

                        //Get recent server
                        int serverId = userRepository.GetRecentServer(userId);
                        if (serverId < 0)
                        {
                            DAL.Models.Server server = serverRepository.GetLatestServer();
                            if (server != null)
                            {
                                string gameUrl = gameAPI.GetGameUrl(userId, username, serverId.ToString());

                                //update server & redirect to game
                                if (!string.IsNullOrWhiteSpace(gameUrl))
                                {
                                    userRepository.UpdatePlayedServer(userId, serverId);
                                    model.Message = gameUrl;
                                }
                            }
                        }
                    }
                    else
                    {
                        model.Message = "Thông tin không hợp lệ";
                    }
                }
                else
                {
                    model.Message = checkValidMessage;
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public JsonResult CheckCaptcha(string captchaId, string instanceId,
        //  string userInput)
        //{
        //    bool ajaxValidationResult = CaptchaControl.AjaxValidate(captchaId, userInput, instanceId);

        //    return Json(ajaxValidationResult, JsonRequestBehavior.AllowGet);
        //}

        string ValidateCaptcha(string captchaId, string instanceId,  string userInput)
        {
            string value = "";
            //bool ajaxValidationResult = CaptchaControl.AjaxValidate(captchaId, userInput, instanceId);
            //if (!ajaxValidationResult)
            //{
            //    value = "Mã xác nhận không hợp lệ";
            //}

            return value;
        }

        public JsonResult CheckLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}