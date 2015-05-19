using Core;
using Core.Constants;
using Core.Providers;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models;

namespace Web.FrontEnd.Controllers
{
    public class LauncherController : Controller
    {
        PostRepository postRepository = new PostRepository();
        BannerRepository bannerRepository = new BannerRepository();

        public LauncherController()
        {

        }

        #region Index
        [HttpGet]
        public ActionResult Index()
        {
            LauncherModel launcherModel = new LauncherModel()
            {
                NewsList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.News, Enums.Status.Active, 5),
                EventList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.Event, Enums.Status.Active, 5),
                BannerList = bannerRepository.GetList(GlobalConstants.GameId, Enums.Status.Active, 5)
            };

            return View(launcherModel);
        }

        [HttpPost]
        public ActionResult Index(LauncherModel launcherModel)
        {
            string message = CheckLoginValid(launcherModel);

            if (string.IsNullOrEmpty(message))
            {
                UserRepository userRepository = new UserRepository();

                int flag = 0;
                int userId = userRepository.Login(launcherModel.Username, launcherModel.Password, ref flag);

                if (userId > 0 && flag == 0)
                {
                    //create authen
                    string userData = launcherModel.Username + "&&&" + userId.ToString();
                    CommonProvider.AuthenticationProvider.CreateAuthentication(launcherModel.Username, DateTime.Now, DateTime.Now.AddMinutes(30),
                        false, userData);

                    return RedirectToAction("ServerList");
                }
                else
                {
                    launcherModel.Message = "Thông tin không hợp lệ";
                }
            }
            else
            {
                launcherModel.Message = message;
            }

            launcherModel.NewsList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.News, Enums.Status.Active, 5);
            launcherModel.EventList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.Event, Enums.Status.Active, 5);
            launcherModel.BannerList = bannerRepository.GetList(GlobalConstants.GameId, Enums.Status.Active, 5);

            return View(launcherModel);
        }

        string CheckLoginValid(LauncherModel launcherModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(launcherModel.Username))
            {
                message = "Nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(launcherModel.Password))
            {
                message = "Nhập mật khẩu";
            }
            else if (string.IsNullOrWhiteSpace(launcherModel.Captcha))
            {
                message = "Nhập mã xác nhận";
            }
            else if (launcherModel.Captcha != SessionManager.Captcha)
            {
                message = "Mã xác nhận không hợp lệ";
            }

            return message;
        }
        #endregion

        #region Server
        [HttpGet]
        public async Task<ActionResult> ServerList()
        {
            if (await AuthHelper.CheckLogin())
            {
                ServerRepository serverRepository = new ServerRepository();
                UserRepository userRepository = new UserRepository();

                LauncherServerModel launcherModel = new LauncherServerModel()
                {
                    ServerList = serverRepository.GetList(),
                    RecentServer = serverRepository.GetById(userRepository.GetRecentServer(AuthHelper.GetUserId()))
                };

                return View(launcherModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}