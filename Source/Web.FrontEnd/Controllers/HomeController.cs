using Core;
using Core.Constants;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.App_Code.Controllers;
using Web.FrontEnd.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace Web.FrontEnd.Controllers
{
    public class HomeController : FrontEndController
    {
        ServerRepository serverRepository = new ServerRepository();
        BannerRepository bannerRepository = new BannerRepository();
        PostRepository postRepository = new PostRepository();

        [MinifyHtml]
        public async Task<ActionResult> Index()
        {
            int total = 0;

            HomeModel homeModel = new HomeModel()
            {
                ServerList = serverRepository.GetList(),
                BannerList = bannerRepository.GetList(GlobalConstants.GameId, Enums.Status.Active, 1, 10, ref total),
                NewsList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.News, Enums.Status.Active, 7),
                EventList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.Event, Enums.Status.Active, 7),
                FaqList = postRepository.GetTop(GlobalConstants.GameId, (int)Enums.PostType.FAQ, Enums.Status.Active, 5),
                
            };

            //if (await AuthHelper.CheckLogin())
            //{
            //    homeModel.CurrentKNB = Factory.UserRepository.GetGold(AuthHelper.GetUserId());
            //}

            return View(homeModel);
        }
        
    }
}
