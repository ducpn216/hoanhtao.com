using Core;
using Core.API;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models;

namespace Web.FrontEnd.Controllers
{
    public class GameController : Controller
    {
        public async Task<ActionResult> Index(int? serverId)
        {
            bool isLoginValid = await AuthHelper.CheckLogin();

            GameModel gameModel = new GameModel();
            bool isValid = false;


            if (isLoginValid)
            {
                UserRepository userRepository = new UserRepository();
                ServerRepository serverRepository = new ServerRepository();
                DAL.Models.Server server = null;
                int userId = AuthHelper.GetUserId();

                if (serverId.HasValue && serverId.Value > 0)
                {
                    //userId = userRepository.GetAccountId(User.Identity.Name);

                    if (userId > 0)
                    {
                        server = serverRepository.GetById(serverId.Value);
                        if (server != null)
                        {
                            GameAPI gameAPI = new GameAPI();
                            string gameUrl = gameAPI.GetGameUrl(userId, User.Identity.Name, serverId.ToString(), server.IsReal);

                            if (gameUrl != "")
                            {
                                isValid = true;
                                gameModel.Url = gameUrl;
                                //HttpContext.Response.Redirect(gameUrl);
                            }
                        }
                    }
                }
                else
                {
                    userId = AuthHelper.GetUserId();
                    if (userId > 0)
                    {
                        serverId = userRepository.GetRecentServer(userId);
                        server = serverRepository.GetById(serverId.Value);
                    }
                }

                if (server != null && userId > 0)
                {
                    GameAPI gameAPI = new GameAPI();
                    string gameUrl = gameAPI.GetGameUrl(userId, User.Identity.Name, serverId.ToString(), server.IsReal);

                    if (gameUrl != "")
                    {
                        //HttpContext.Response.Redirect(gameUrl);
                        isValid = true;
                        gameModel.Url = gameUrl;
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

            if (isValid)
            {
                return View(gameModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> Test()
        {
            bool isLoginValid = await AuthHelper.CheckLogin();

            GameModel gameModel = new GameModel();
            bool isValid = false;


            if (isLoginValid && AuthHelper.CheckAdmin(AuthHelper.GetUsername()))
            {

                GameAPI gameAPI = new GameAPI();
                string gameUrl = gameAPI.GetGameUrl(AuthHelper.GetUserId(), User.Identity.Name, "999");

                if (gameUrl != "")
                {
                    isValid = true;
                    gameModel.Url = gameUrl;
                    //HttpContext.Response.Redirect(gameUrl);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            if (isValid)
            {
                return View("Index", gameModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //public async Task<ActionResult> Index(int? serverId)
        //{
        //    bool isLoginValid = await AuthHelper.CheckLogin();

        //    if (isLoginValid && AuthHelper.CheckAdmin(User.Identity.Name))
        //    {
        //        UserRepository userRepository = new UserRepository();
        //        ServerRepository serverRepository = new ServerRepository();
        //        DAL.Models.Server server = null;
        //        int userId = AuthHelper.GetUserId();

        //        if (serverId.HasValue && serverId.Value > 0)
        //        {
        //            //userId = userRepository.GetAccountId(User.Identity.Name);

        //            if (userId > 0)
        //            {
        //                server = serverRepository.GetById(serverId.Value);
        //                if (server != null)
        //                {
        //                    GameAPI gameAPI = new GameAPI();
        //                    string gameUrl = gameAPI.GetGameUrl(userId, User.Identity.Name, serverId.ToString(), server.IsReal);

        //                    if (gameUrl != "")
        //                    {
        //                        HttpContext.Response.Redirect(gameUrl);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            userId = AuthHelper.GetUserId();
        //            if (userId > 0)
        //            {
        //                serverId = userRepository.GetRecentServer(userId);
        //                server = serverRepository.GetById(serverId.Value);
        //            }
        //        }

        //        if (server != null && userId > 0)
        //        {
        //            GameAPI gameAPI = new GameAPI();
        //            string gameUrl = gameAPI.GetGameUrl(userId, User.Identity.Name, serverId.ToString(), server.IsReal);

        //            if (gameUrl != "")
        //            {
        //                HttpContext.Response.Redirect(gameUrl);
        //            }
        //        }
        //    }

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
