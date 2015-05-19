using AutoMapper;
using Core;
using Core.Constants;
using Core.Providers;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;
using Web.BackEnd.App_Code.Attributes;
using Web.BackEnd.Models;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{
    //[CustomAuthorize(ForbiddenGroupId = new[] { 2, 3 })]
    public class PostController : BackEndController
    {
        PostRepository postRepository = new PostRepository();
        GameRepository gameRepository = new GameRepository();

        const int POST_PAGE_COUNT = 5;
        const int POST_PAGE_SIZE = 10;

        public PostController()
        {
            Mapper.CreateMap<Post, PostModel>();
            Mapper.CreateMap<PostModel, Post>();
            //Mapper.CreateMap<PagedList<PostModel>, PagedList<Post>>();
            //Mapper.CreateMap<PagedList<Post>, PagedList<PostModel>>();
        }

        [HttpGet]
        public ActionResult Index(int? postTypeId = null, int? categoryId = null, int page = 1)
        {
            PostListModel postListModel = new PostListModel()
            {
                PostTypeList = BackEndUtilities.GetPostTypeList(),
                CategoryList = BackEndUtilities.GetCategoryList()
            };

            if (postTypeId.HasValue)
            {
                int postTotal = 0;
                Enums.PostType? postType = Utilities.GetPostType(postTypeId.Value);
                categoryId = categoryId.HasValue ? categoryId.Value : 0;

                if (postType.Value == Enums.PostType.News || postType.Value == Enums.PostType.Event)
                {
                    categoryId = null;
                }

                if (postType.HasValue)
                {
                    List<Post> postList = postRepository.GetList(GlobalConstants.GameId, categoryId, postTypeId.Value,
                    null, page, BackendConstants.POST_PAGE_SIZE, ref postTotal);

                    if (postList.Count > 0)
                    {
                        foreach (var post in postList)
                        {
                            postListModel.PostList.Add(Mapper.Map<Post, PostModel>(post));
                        }

                        string queryString = String.Format("?postTypeId={0}&categoryId={1}&", postTypeId.Value,
                            categoryId.HasValue ? categoryId.Value : -1);

                        ViewBag.Paging = CommonProvider.FunctionProvider.DrawPaging2(page, POST_PAGE_SIZE, POST_PAGE_COUNT,
                            postTotal, 0, queryString, "Sau", "Trước");
                    }

                    postListModel.SelectedPostTypeId = postTypeId.Value;
                    postListModel.SelectedCategoryId = categoryId.HasValue ? categoryId.Value : -1;
                }
            }

            return View(postListModel);
        }

        [HttpGet]
        public ActionResult Edit(int? postId)
        {
            PostFormModel postFormModel = new PostFormModel()
            {
                CategoryList = BackEndUtilities.GetCategoryList(),
                PostTypeList = BackEndUtilities.GetPostTypeList()
            };

            if (postId.HasValue)
            {
                Post post = postRepository.GetById(postId.Value);
                postFormModel.SelectedCategoryId = (post.CategoryId.HasValue) ? post.CategoryId.Value : 0;
                postFormModel.SelectedPostTypeId = post.PostTypeId;


                if (post != null)
                {
                    postFormModel.PostModel = Mapper.Map<Post, PostModel>(post);
                    postFormModel.PostModel.Image = FilePathManager.GetAvatarFilePath(post.PostId, post.ImagePath, FolderPathConstansts.PostFolderPath);
                }
                else
                {
                    return RedirectToAction("Index");
                }

                ViewBag.SubmitButtonText = "Cập nhật";
            }
            else
            {
                ViewBag.SubmitButtonText = "Thêm";
            }

            return View(postFormModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(PostFormModel postFormModel)
        {
            if (postFormModel != null)
            {
                HttpPostedFileBase postedFile = Request.Files["image"];
                string validateMessage = CheckEditPostValid(postFormModel);


                if (string.IsNullOrWhiteSpace(validateMessage))
                {
                    string fileName = "";
                    if (postedFile != null && postedFile.ContentLength > 50)
                    {
                        TimeSpan timeSpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        fileName = timeSpan.TotalSeconds.ToString() + Path.GetExtension(postedFile.FileName);
                    }

                    Post post = Mapper.Map<PostModel, Post>(postFormModel.PostModel);
                    post.GameId = GlobalConstants.GameId;
                    post.PostTypeId = postFormModel.SelectedPostTypeId;
                    post.CategoryId = postFormModel.SelectedCategoryId;
                    post.ImagePath = fileName;
                    post.CreatedBy = 0;
                    post.CreatedDate = DateTime.Now;
                    post.Status = postFormModel.PostModel.Status;

                    if (postFormModel.PostModel.PostId > 0)
                    {
                        if (postRepository.Update(post))
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thành công", Enums.ErrorCode.Success);
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thất bại", Enums.ErrorCode.Error);
                        }
                    }
                    else
                    {
                        post.PostId = postRepository.Insert(post);
                        if (post.PostId > 0)
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thành công", Enums.ErrorCode.Success);
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thất bại", Enums.ErrorCode.Error);
                        }
                    }

                    if (postedFile != null && postedFile.ContentLength > 50)
                    {
                        Enums.UploadStatus status = UploadProvider.Upload(post.PostId, postedFile, FolderPathConstansts.PostFolderPath, fileName);
                        if (status == Enums.UploadStatus.Success)
                        {
                            postFormModel.PostModel.Image = FilePathManager.GetAvatarFilePath(post.PostId, post.ImagePath, FolderPathConstansts.PostFolderPath);
                        }
                    }


                    if (postFormModel.PostModel.PostId <= 0)
                    {
                        return RedirectToAction("Index", new { postTypeId = postFormModel.SelectedPostTypeId, categoryId = postFormModel.SelectedCategoryId });
                    }
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage(validateMessage, Enums.ErrorCode.Error);
                }

                postFormModel.CategoryList = BackEndUtilities.GetCategoryList(postFormModel.SelectedCategoryId);
                postFormModel.PostTypeList = BackEndUtilities.GetPostTypeList();
            }


            return View(postFormModel);
        }

        [HttpGet]
        public ActionResult Delete(int? postId, int? postTypeId, int? categoryId)
        {
            if (postId.HasValue)
            {
                if (postRepository.Delete(postId.Value))
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Xóa thành công", Enums.ErrorCode.Success);

                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Xóa thất bại", Enums.ErrorCode.Error);
                }
            }

            return RedirectToAction("Index", new { postTypeId = postTypeId, categoryId = categoryId });
        }

        string CheckEditPostValid(PostFormModel postFormModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(postFormModel.PostModel.Title))
            {
                message = "Nhập tiêu đề<br/>";
            }

            if (string.IsNullOrWhiteSpace(postFormModel.PostModel.Title))
            {
                message += "Nhập nội dung<br/>";
            }

            if (postFormModel.SelectedPostTypeId <= 0)
            {
                message += "Chọn loại<br/>";
            }

            return message;
        }
    }
}
