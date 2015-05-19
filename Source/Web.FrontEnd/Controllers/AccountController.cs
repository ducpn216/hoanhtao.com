using Core;
using Core.API;
using Core.Constants;
using Core.Providers;
using Core.Providers.Google;
using Core.Providers.Google.Requests;
using Core.Providers.Google.Responses;
using DAL.Models;
using DAL.Reponses;
using DAL.Repositories;
using Facebook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.App_Code.Attributes;
using Web.FrontEnd.Models;
using Web.FrontEnd.Models.Account;

namespace Web.FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                //Url.Action(OauthConstants.FB_CALLBACK);
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = OauthConstants.FacebookCallback;
                return uriBuilder.Uri;
            }
        }

        UserRepository userRepository = new UserRepository();
        ServerRepository serverRepository = new ServerRepository();
        QuestionRepository questionRepository = new QuestionRepository();
        GameAPI gameAPI = new GameAPI();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            //if (Session["facebooktoken"] != null)
            //{
            //    var fb = new Facebook.FacebookClient();
            //    string accessToken = Session["facebooktoken"] as string;

            //    var logoutUrl = fb.GetLogoutUrl(new { access_token = accessToken, next = "http://hoanh.goblin.vn/" });

            //    Session.RemoveAll();
            //    return Redirect(logoutUrl.AbsoluteUri);
            //}

            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #region Login

        string ValidateLogin(LoginModel loginModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(loginModel.Username))
            {
                message = "Nhập username<br/>";
            }
            
            if (string.IsNullOrEmpty(loginModel.Password))
            {
                message += "Nhập mật khẩu<br/>";
            }

            if (string.IsNullOrWhiteSpace(loginModel.Captcha))
            {
                message += "Nhập mã xác nhận<br/>";
            }
            else if (SessionManager.Captcha != loginModel.Captcha)
            {
                message += "Mã xác nhận không hợp lệ<br/>";
            }
            //else if (loginModel.ShowCaptcha)
            //{
                
            //}

            return message;
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            LoginModel loginModel = new LoginModel();
            bool flag = await AuthHelper.CheckLogin();

            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }

            return View(loginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            string message = ValidateLogin(loginModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                int flag = 0;
                int userId = userRepository.Login(loginModel.Username, loginModel.Password, ref flag);
                if (userId > 0)
                {
                    Session.RemoveAll();
                    Session.Abandon();
                    FormsAuthentication.SignOut();

                    string userData = loginModel.Username + "&&&" + userId.ToString();
                    CommonProvider.AuthenticationProvider.CreateAuthentication(loginModel.Username, DateTime.Now, DateTime.Now.AddMinutes(30),
                        false, userData);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Message"] = "Thông tin không hợp lệ";
                }
            }
            else
            {
                TempData["Message"] = message;
            }

            return View(loginModel);
        }

        #endregion

        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            RegisterModel registerAccountModel = new RegisterModel();
            return View(registerAccountModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (registerModel != null)
            {
                string message = ValidateRegister(registerModel);

                if (string.IsNullOrEmpty(message))
                {
                    int flag = -1;
                    string pid = "";

                    //if (SessionManager.PID != null && !string.IsNullOrWhiteSpace(SessionManager.PID))
                    //{
                    //    pid = SessionManager.PID;
                    //}
                    if (Request.QueryString["pid"] != null && !string.IsNullOrEmpty(Request.QueryString["pid"]))
                    {
                        pid = Request.QueryString["pid"];
                    }

                    int userId = userRepository.Register(registerModel.Username, registerModel.Password, registerModel.Password,
                        "fast", 1, "fast", "fast", "fast", "fast", "fast", pid, ref flag, ref message);

                    if (flag == 0)
                    {
                        string userData = registerModel.Username + "&&&" + userId.ToString();
                        CommonProvider.AuthenticationProvider.CreateAuthentication(registerModel.Username, DateTime.Now, DateTime.Now.AddMinutes(30),
                            false, userData);

                        TempData["Message"] = "Đăng ký thành công";
                        registerModel.IsSuccess = true;
                        SessionManager.PID = null;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Message"] = message;
                    }
                }
                else
                {
                    TempData["Message"] = message;
                }
            }

            return View(registerModel);
        }

        string ValidateRegister(RegisterModel registerModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(registerModel.Username))
            {
                message = "Nhập tài khoản<br/>";
            }

            if (string.IsNullOrEmpty(registerModel.Password))
            {
                message += "Nhập mật khẩu<br/>";
            }
            else if (string.IsNullOrEmpty(registerModel.ConfirmPassword))
            {
                message += "Xác nhận mật khẩu<br/>";
            }

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                message += "Xác nhận mật khẩu không hợp lệ<br/>";
            }
            else if (string.IsNullOrWhiteSpace(registerModel.Captcha))
            {
                message += "Nhập mã xác nhận<br/>";
            }

            if (registerModel.Captcha != SessionManager.Captcha)
            {
                message += "Mã xác nhận không hợp lệ<br/>";
            }

            return message;
        }
        #endregion

        #region ChangePassword
        [HttpGet]
        [CustomAuthorize(LinkManager.CHANGE_PASSWORD)]
        public ActionResult ChangePassword()
        {
            ChangePasswordModel changePasswordModel = new ChangePasswordModel();

            return View(changePasswordModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                ResultResponse response = userRepository.ChangePassword2(User.Identity.Name, model.CurrentPassword, model.NewPassword);
                if (response.Flag == 0)
                {
                    model.Message = "Đổi mật khẩu thành công";
                    model.IsValid = true;
                }
                else
                {
                    model.Message = response.Message;
                }
            }
            else
            {
                model.Message = Helper.GetErrorsOfModelState(ModelState);
            }

            return View(model);
        }
        #endregion

        #region Profile

        [CustomAuthorize(LinkManager.PROFILE)]
        public ActionResult Edit()
        {
            ProfileModel model = new ProfileModel();

            DataSet dataSet = userRepository.GetUserInfo(AuthHelper.GetUserId());
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];
                model.Email = row["Email"].ToString();
                model.Phone = row["PhoneNumber"].ToString();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                ResultResponse response = userRepository.UpdateProfile(AuthHelper.GetUserId(), model.Email, "", model.Phone);
                if (response.Flag == 0)
                {
                    model.Message = "Cập nhật thông tin thành công";
                    model.IsValid = true;
                }
                else
                {
                    model.Message = response.Message;
                }
            }
            else
            {
                model.Message = Helper.GetErrorsOfModelState(ModelState);
            }

            return View(model);
        }

        #endregion

        #region ForgetPassword
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            ForgetPasswordModel model = new ForgetPasswordModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                ResultResponse response = userRepository.ForgetPassword(model.Username, model.Email);
                if (response.Flag == 0)
                {
                    model.IsValid = true;
                    model.Message = "Mật khẩu mới: " + response.Message;
                }
                else
                {
                    model.Message = response.Message;
                }
            }
            else
            {
                model.Message = Helper.GetErrorsOfModelState(ModelState);
            }

            return View(model);
        }
        #endregion

        #region LoginFB
        //[AllowAnonymous]
        //public ActionResult LoginFacebook()
        //{
        //    //RedirectUri.AbsoluteUri
        //    var fb = new FacebookClient();
        //    var loginUrl = fb.GetLoginUrl(new
        //    {
        //        client_id = OauthConstants.FacebookAppId,
        //        client_secret = OauthConstants.FacebookAppSecret,
        //        redirect_uri = OauthConstants.FacebookCallback,
        //        response_type = "code",
        //        scope = "email"
        //    });

        //    return Redirect(loginUrl.AbsoluteUri);
        //}

        //public ActionResult FacebookCallback(string code)
        //{
        //    var fb = new FacebookClient();
        //    dynamic result = fb.Post("oauth/access_token", new
        //    {
        //        client_id = OauthConstants.FacebookAppId,
        //        client_secret = OauthConstants.FacebookAppSecret,
        //        redirect_uri = OauthConstants.FacebookCallback,
        //        code = code
        //    });

        //    var accessToken = result.access_token;

        //    // Store the access token in the session for farther use
        //    SessionManager.FacebookAccessToken = accessToken;

        //    // update the facebook client with the access token so 
        //    // we can make requests on behalf of the user
        //    fb.AccessToken = accessToken;

        //    // Get the user's information, like email, first name, middle name etc
        //    dynamic me = fb.Get("me?fields=id,email,first_name,last_name");
        //    string email = me.email;
        //    string id = me.id;
        //    string firstName = me.first_name;
        //    string lastName = me.last_name;

        //    FileLog.Write("FB", "Login", "ID: " + me.id);

        //    // Set the auth cookie
        //    int flag = -1;
        //    int userId = userRepository.LoginSocial(id, Enums.LoginType.Facebook, ref flag);

        //    if (userId > 0)
        //    {
        //        string fullName = firstName + " " + lastName;
        //        string userData = fullName + "&&&" + userId.ToString();
        //        CommonProvider.AuthenticationProvider.CreateAuthentication(id, DateTime.Now, DateTime.Now.AddMinutes(30),
        //            false, userData);

        //        //Get recent server
        //        int serverId = userRepository.GetRecentServer(userId);
        //        if (serverId < 0)
        //        {
        //            serverId = 1;
        //        }

        //        string gameUrl = gameAPI.GetGameUrl(userId, email, serverId.ToString());
        //        if (!string.IsNullOrEmpty(gameUrl))
        //        {
        //            return Redirect(gameUrl);
        //        }
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        #endregion

        #region LoginGG

        //public ActionResult LoginGoogle()
        //{
        //    GoogleProvider provider = new GoogleProvider();
        //    TokenRequest request = new TokenRequest()
        //    {
        //        Scope = "email+profile+https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fplus.login",
        //        ApprovalPrompt = "force"
        //    };

        //    string url = provider.GetLoginUrl(request);

        //    if (!string.IsNullOrEmpty(url))
        //    {
        //        return Redirect(url);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        //public async Task<ActionResult> GoogleCallback()
        //{
        //    try
        //    {
        //        GoogleProvider googleProvider = new GoogleProvider();

        //        string googleAccessToken = await googleProvider.GetAccessToken();
        //        if (!string.IsNullOrEmpty(googleAccessToken))
        //        {
        //            SessionManager.GoogleAccessToken = googleAccessToken;

        //            ProfileResponse profileResponse = await googleProvider.GetProfile(googleAccessToken);
        //            if (profileResponse != null)
        //            {
        //                int flag = -1;
        //                int userId = userRepository.LoginSocial(profileResponse.id, Enums.LoginType.Google, ref flag);

        //                if (userId > 0)
        //                {
        //                    string userData = profileResponse.displayName + "&&&" + userId.ToString();
        //                    CommonProvider.AuthenticationProvider.CreateAuthentication(profileResponse.id, DateTime.Now, DateTime.Now.AddMinutes(30),
        //                        false, userData);

        //                    int serverId = userRepository.GetRecentServer(userId);
        //                    if (serverId < 0)
        //                    {
        //                        serverId = 1;
        //                    }

        //                    string gameUrl = gameAPI.GetGameUrl(userId, profileResponse.id, serverId.ToString());
        //                    if (!string.IsNullOrEmpty(gameUrl))
        //                    {
        //                        return Redirect(gameUrl);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        FileLog.Write("Google", "Callback", ex.ToString());
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        #endregion

        public List<SelectListItem> GetQuestionList(int? selectedQuestionId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<Question> questionList = questionRepository.GetList(Enums.Status.Active);

            foreach (var question in questionList)
            {
                bool selected = false;

                if (selectedQuestionId.HasValue && question.QuestionId == selectedQuestionId.Value)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = question.QuestionName, Value = question.QuestionId.ToString(), Selected = selected });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Chọn câu hỏi", Value = "" });

            return itemList;
        }
    }
}
