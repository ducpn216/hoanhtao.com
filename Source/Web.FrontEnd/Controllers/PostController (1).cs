using Core;
using Core.Constants;
using Core.Providers;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.Models;

namespace Web.FrontEnd.Controllers
{
    public class PostController : Controller
    {
        int PAGE_SIZE = 5;
        int PAGE_COUNT = 5;
        int OTHERS_COUNT = 5;

        PostRepository postRepository = new PostRepository();

        public PostController()
        {

        }

        public ActionResult Index(int postTypeId = 1, int page = 1)
        {
            PostListModel postListModel = new PostListModel()
            {
                PostTypeId = postTypeId
            };

            int total = 0;

            postListModel.PostList = postRepository.GetList(GlobalConstants.GameId, 0, postTypeId,
                    Enums.Status.Active, page, PAGE_SIZE, ref total);

            if (postListModel.PostList != null && postListModel.PostList.Count > 0)
            {
                ViewBag.Paging = CommonProvider.FunctionProvider.DrawPaging2(page, PAGE_SIZE, PAGE_COUNT, total, 0,
                "?", "Sau", "Trước");
            }
            
            return View(postListModel);
        }

        public ActionResult Detail(int postId)
        {
            PostDetailModel postDetailModel = new PostDetailModel();

            Post post = postRepository.GetById(postId);
            if (post != null)
            {
                postDetailModel.PostId = post.PostId;
                postDetailModel.Title = post.Title;
                postDetailModel.Content = post.Content;
                postDetailModel.PostTypeId = post.PostTypeId;

                postDetailModel.OtherPostList = postRepository.GetOthers(post.PostId, 0, post.PostTypeId, Enums.Status.Active, OTHERS_COUNT);
            }

            return View(postDetailModel);
        }

        //public ActionResult CamNang(int? categoryId)
        //{
        //    CamNangListModel camnangModel = new CamNangListModel();

        //    if (categoryId.HasValue && categoryId.Value > 0)
        //    {
        //        Post post = postRepository.GetFirstPost(categoryId.Value);
        //        if (post != null)
        //        {
        //            camnangModel.Title = post.Title;
        //            camnangModel.Content = post.Content;
        //            camnangModel.PostTypeId = post.PostTypeId;
        //            camnangModel.PostList = postRepository.GetList(post.PostTypeId, categoryId.Value);

        //            camnangModel.OtherPostList = postRepository.GetOthers(post.PostId, 0, post.PostTypeId, Enums.Status.Active, OTHERS_COUNT);
        //        }
        //    }


        //    return View(camnangModel);
        //}

        //public ActionResult CamNangDetail(int? postId)
        //{
        //    CamNangListModel camnangModel = new CamNangListModel();

        //    if (postId.HasValue && postId.Value > 0)
        //    {
        //        Post post = postRepository.GetById(postId.Value);
        //        if (post != null)
        //        {
        //            camnangModel.PostId = post.PostId;
        //            camnangModel.PostTypeId = post.PostTypeId;
        //            camnangModel.Title = post.Title;
        //            camnangModel.Content = post.Content;
        //            camnangModel.PostList = postRepository.GetOthers(postId.Value);

        //            camnangModel.OtherPostList = postRepository.GetOthers(post.PostId, post.CategoryId.Value, post.PostTypeId, Enums.Status.Active, OTHERS_COUNT);
        //        }
        //    }


        //    return View("CamNang", camnangModel);
        //}
    }
}