using Core;
using Core.Constants;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.Models;

namespace Web.FrontEnd.Controllers
{
    public class PlayGameController : Controller
    {
        const int BANNER_COUNT = 5;
        const int POST_COUNT = 5;

        public PlayGameController()
        {

        }

        public ActionResult Index()
        {
            //if (AuthHelper.CheckAdmin(User.Identity.Name))
            //{
                
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            PlayGameModel playGameModel = new PlayGameModel();

            BannerRepository bannerRepository = new BannerRepository();
            ServerRepository serverRepository = new ServerRepository();
            PostRepository postRepository = new PostRepository();

            playGameModel.BannerList = bannerRepository.GetList(GlobalConstants.GameId, Enums.Status.Active, BANNER_COUNT);
            playGameModel.PostList = postRepository.GetList(GlobalConstants.GameId, Enums.Status.Active, POST_COUNT);

            var serverList = serverRepository.GetList(true, true);
            playGameModel.ServerList = serverList;
            playGameModel.NewServerList = serverList.Where(x => x.IsNew).ToList();

            return View(playGameModel);
        }
    }
}