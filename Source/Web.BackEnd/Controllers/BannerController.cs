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
    [CustomAuthorize(ForbiddenGroupId = new[] { 2, 3 })]
    public class BannerController : BackEndController
    {
        BannerRepository bannerRepository = new BannerRepository();

        const int PAGE_COUNT = 5;
        const int PAGE_SIZE = 10;
        int page = 1;

        #region Querystring

        int GetPostTypeId()
        {
            string value = Request.QueryString["postTypeId"];

            if (value != null)
            {
                if (CommonProvider.FunctionProvider.IsValidNumeric(value))
                {
                    return Convert.ToInt32(value);
                }
            }

            return -1;
        }

        int GetPage()
        {
            string value = Request.QueryString["page"];

            if (value != null)
            {
                if (CommonProvider.FunctionProvider.IsValidNumeric(value))
                {
                    return Convert.ToInt32(value);
                }
            }

            return 1;
        }

        #endregion

        public BannerController()
        {
            Mapper.CreateMap<Banner, BannerModel>();
            Mapper.CreateMap<BannerModel, Banner>();
        }

        [HttpGet]
        public ActionResult Index(int? status)
        {
            BannerListModel bannerListModel = new BannerListModel()
            {
                StatusList = BackEndUtilities.GetStatusList()
            };

            Enums.Status? statusEnum = Utilities.GetStatus(status);
            int total = 0;

            List<Banner> bannerList = bannerRepository.GetList(GlobalConstants.GameId, statusEnum, page, PAGE_SIZE, ref total);

            if (bannerList.Count > 0)
            {
                foreach (var banner in bannerList)
                {
                    var bannerModel = Mapper.Map<Banner, BannerModel>(banner);
                    bannerModel.ImagePath = FilePathManager.GetAvatarFilePath(banner.BannerId, banner.ImagePath, FolderPathConstansts.BannerFolderPath);
                    
                    bannerListModel.BannerList.Add(bannerModel);
                }

                ViewBag.Paging = CommonProvider.FunctionProvider.DrawPaging2(page, PAGE_SIZE, PAGE_COUNT, total, 0, "", "Sau", "Trước");
            }

            bannerListModel.SelectedStatus = status.HasValue ? status.Value : 0;

            return View(bannerListModel);
        }

        [HttpGet]
        public ActionResult Edit(int? bannerId)
        {
            BannerFormModel bannerFormModel = new BannerFormModel()
            {
                StatusList = BackEndUtilities.GetStatusList()
            };

            if (bannerId.HasValue)
            {
                Banner banner = bannerRepository.GetById(bannerId.Value);
                if (banner != null)
                {
                    bannerFormModel.BannerModel = Mapper.Map<Banner, BannerModel>(banner);
                    bannerFormModel.SelectedStatus = banner.Status;
                    bannerFormModel.BannerModel.ImagePath = FilePathManager.GetAvatarFilePath(banner.BannerId, banner.ImagePath, FolderPathConstansts.BannerFolderPath);
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

            return View(bannerFormModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(BannerFormModel bannerFormModel)
        {
            if (bannerFormModel != null)
            {
                bannerFormModel.StatusList = BackEndUtilities.GetStatusList(bannerFormModel.SelectedStatus);

                string validateMessage = CheckEditBannerValid(bannerFormModel);
                if (string.IsNullOrWhiteSpace(validateMessage))
                {
                    HttpPostedFileBase postedFile = Request.Files["image"];

                    string fileName = "";
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        TimeSpan timeSpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        fileName = timeSpan.TotalSeconds.ToString() + Path.GetExtension(postedFile.FileName);
                    }

                    Banner banner = Mapper.Map<BannerModel, Banner>(bannerFormModel.BannerModel);
                    banner.GameId = GlobalConstants.GameId;
                    banner.Status = bannerFormModel.BannerModel.Status;
                    banner.ImagePath = fileName;
                    banner.CreatedBy = 0;
                    banner.CreatedDate = DateTime.Now;

                    if (bannerFormModel.BannerModel.BannerId > 0)
                    {
                        if (bannerRepository.Update(banner))
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
                        banner.BannerId = bannerRepository.Insert(banner);
                        if (banner.BannerId > 0)
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thành công", Enums.ErrorCode.Success);
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thất bại", Enums.ErrorCode.Error);
                        }
                    }

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        Enums.UploadStatus status = UploadProvider.Upload(banner.BannerId, postedFile, FolderPathConstansts.BannerFolderPath, fileName);
                        if (status == Enums.UploadStatus.Success)
                        {
                            bannerFormModel.BannerModel.ImagePath = FilePathManager.GetAvatarFilePath(banner.BannerId, banner.ImagePath, FolderPathConstansts.BannerFolderPath);
                        }
                    }

                    if (bannerFormModel.BannerModel.BannerId <= 0)
                    {
                        return RedirectToAction("Index", new { status = bannerFormModel.SelectedStatus });
                    }
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage(validateMessage, Enums.ErrorCode.Error);
                }
            }


            return View(bannerFormModel);
        }

        [HttpGet]
        public ActionResult Delete(int? bannerId, int? status)
        {
            if (bannerId.HasValue)
            {
                if (bannerRepository.Delete(bannerId.Value))
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Xóa thành công", Enums.ErrorCode.Success);

                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Xóa thất bại", Enums.ErrorCode.Error);
                }
            }

            return RedirectToAction("Index", new { status = status });
        }

        string CheckEditBannerValid(BannerFormModel bannerFormModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(bannerFormModel.BannerModel.Title))
            {
                message = "Nhập tiêu đề<br/>";
            }

            if (bannerFormModel.BannerModel.BannerId <= 0)
            {
                HttpPostedFileBase postedFile = Request.Files["image"];
                if (postedFile == null || postedFile.ContentLength <= 0)
                {
                    message += "Chọn hình ảnh<br/>";
                }
                //else
                //{
                //    string extenstion = Path.GetExtension(postedFile.FileName);
                //    if (UploadProvider.IsExtensionValid(extenstion))
                //    {
                //        message = "Định dạng không hợp lệ<br/>";
                //    }
                //}
            }

            return message;
        }
    }
}
