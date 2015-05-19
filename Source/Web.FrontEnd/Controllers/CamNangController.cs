using Core;
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
    public class CamNangController : Controller
    {
        int PAGE_SIZE = 5;
        int PAGE_COUNT = 5;
        int OTHERS_COUNT = 5;

        PostRepository postRepository = new PostRepository();
        CategoryRepository categoryRepository = new CategoryRepository();

        public ActionResult Index()
        {
            CamNangListModel camnangModel = new CamNangListModel();

            return View(camnangModel);

        }
        public ActionResult List(int? categoryId)
        {
            CamNangListModel camnangModel = new CamNangListModel();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                Post post = postRepository.GetFirstPost(categoryId.Value, (int)Enums.PostType.CamNang);
                if (post != null)
                {
                    camnangModel.Title = post.Title;
                    camnangModel.Content = post.Content;
                    camnangModel.PostTypeId = post.PostTypeId;
                    camnangModel.PostList = postRepository.GetList(post.PostTypeId, categoryId.Value);

                    camnangModel.OtherPostList = postRepository.GetOthers(post.PostId, 0, post.PostTypeId, Enums.Status.Active, OTHERS_COUNT);
                }

                
                int parentId = categoryRepository.GetParentIdByCategoryId(categoryId.Value);
                camnangModel.SelectedCategoryId = parentId;
            }


            return View("Detail", camnangModel);
        }

        public ActionResult Detail(int? postId)
        {
            CamNangListModel camnangModel = new CamNangListModel();

            if (postId.HasValue && postId.Value > 0)
            {
                Post post = postRepository.GetById(postId.Value);
                if (post != null)
                {
                    camnangModel.PostId = post.PostId;
                    camnangModel.PostTypeId = post.PostTypeId;
                    camnangModel.Title = post.Title;
                    camnangModel.Content = post.Content;
                    camnangModel.PostList = postRepository.GetOthers(postId.Value);

                    camnangModel.OtherPostList = postRepository.GetOthers(post.PostId, post.CategoryId.Value, post.PostTypeId, Enums.Status.Active, OTHERS_COUNT);
                }

                int categoryId = categoryRepository.GetCategoryIdByPostId(postId.Value);
                int parentId = categoryRepository.GetParentIdByCategoryId(categoryId);
                camnangModel.SelectedCategoryId = parentId;
                
            }


            return View(camnangModel);
        }
    }
}