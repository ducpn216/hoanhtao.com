using Core;
using Core.Constants;
using Core.Providers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;
using Web.FrontEnd.App_Code.Attributes;
using Web.FrontEnd.App_Code.Controllers;
using Web.FrontEnd.Models.Support;

namespace Web.FrontEnd.Controllers
{
    public class SupportController : FrontEndController
    {
        int PAGE_SIZE = 10;
        int PAGE_COUNT = 5;

        public SupportController()
        {

        }

        [CustomAuthorize(LinkManager.SUPPORT)]
        public ActionResult Index(int statusId = (int)Enums.SupportStatus.Resolved, int page = 1)
        {
            SupportListModel model = new SupportListModel()
            {
                StatusList = FrontEndUtils.GetSupportStatusList(null),
                SelectedStatus = statusId
            };

            //url = @LinkManager.sup + "status=" + statusId.ToString() + "&";
            int total = 10;

            model.DataList = Factory.SupportRepository.GetListByStatus(statusId, page, PAGE_SIZE, ref total);
            TempData["Paging"] = CommonProvider.FunctionProvider.DrawPaging2(page, PAGE_SIZE, PAGE_COUNT, total, 0, "?", "Sau", "Trước");

            return View(model);
        }

        #region Feedback
        [CustomAuthorize(LinkManager.SUPPORT_FEEDBACK)]
        public ActionResult Feedback()
        {
            SupportFeedbackModel model = new SupportFeedbackModel()
            {
                ServerList = FrontEndUtils.GetServerList(),
                SupportTypeList = FrontEndUtils.GetSupportTypeList(null)
            };

            return View(model);
        }

        [CustomAuthorize(LinkManager.SUPPORT_FEEDBACK)]
        [HttpPost]
        public ActionResult Feedback(SupportFeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile1 = Request.Files["image1"];
                HttpPostedFileBase postedFile2 = Request.Files["image2"];
                HttpPostedFileBase postedFile3 = Request.Files["image3"];

                string image1 = "";
                if (postedFile1 != null && postedFile1.ContentLength > 50)
                {
                    image1 = "image1" + Path.GetExtension(postedFile1.FileName);
                }

                string image2 = "";
                if (postedFile2 != null && postedFile2.ContentLength > 50)
                {
                    image2 = "image2" + Path.GetExtension(postedFile2.FileName);
                }

                string image3 = "";
                if (postedFile3 != null && postedFile3.ContentLength > 50)
                {
                    image3 = "image3" + Path.GetExtension(postedFile3.FileName);
                }

                int supportId = Factory.SupportRepository.Insert(AuthHelper.GetUserId(), model.SelectedServerId, model.Title,
                    (int)Enums.SupportStatus.NotResolve, (short)model.SelectedSupportTypeId);

                if (supportId > 0)
                {
                    SupportDetail detail = new SupportDetail()
                    {
                        UserId = AuthHelper.GetUserId(),
                        CreatedDate = DateTime.Now,
                        Content = model.Content,
                        SupportId = supportId,
                        Role = false,
                        Image1 = image1,
                        Image2 = image2,
                        Image3 = image3
                    };

                    int detailID = Factory.SupportRepository.InsertSupportDetail(detail);

                    if (detailID > 0)
                    {
                        //Factory.SupportRepository.UpdateSupportStatus(supportId, Enums.SupportStatus.NotResolve);

                        if (postedFile1 != null && postedFile1.ContentLength > 50)
                        {
                            Enums.UploadStatus status = UploadProvider.Upload(detailID, postedFile1, FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, image1);
                        }

                        if (postedFile2 != null && postedFile2.ContentLength > 50)
                        {
                            Enums.UploadStatus status = UploadProvider.Upload(detailID, postedFile2, FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, image2);
                        }

                        if (postedFile1 != null && postedFile1.ContentLength > 50)
                        {
                            Enums.UploadStatus status = UploadProvider.Upload(detailID, postedFile1, FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, image3);
                        }

                        model.Message = "Gửi hỗ trợ thành công";
                        model.IsValid = true;
                    }
                    else
                    {
                        model.Message = "Có lỗi trong quá trình xử lý";
                    }
                }
                else
                {
                    model.Message = "Có lỗi trong quá trình xử lý";
                }
            }
            else
            {
                model.Message = Helper.GetErrorsOfModelState(ModelState);
            }

            model.ServerList = FrontEndUtils.GetServerList();
            model.SupportTypeList = FrontEndUtils.GetSupportTypeList(null);

            return View(model);
        }
        #endregion

        #region Detail
        [CustomAuthorize(LinkManager.SUPPORT)]
        public ActionResult Detail(int? supportId = null)
        {
            SupportDetailModel model = new SupportDetailModel();

            if (supportId.HasValue)
            {
                Support support = Factory.SupportRepository.GetSupportById(AuthHelper.GetUserId(), supportId.Value);
                if (support != null)
                {
                    model.Support = support;
                    model.SupportDetailList = Factory.SupportRepository.GetSupportDetailList(AuthHelper.GetUserId(), supportId.Value);

                    if (support.Status == (int)Enums.SupportStatus.Resolved)
                    {
                        model.IsResolved = true;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(int? supportId, SupportDetailModel model)
        {
            //int supportId = -1;

            //if (Request.QueryString["supportId"] != null && int.TryParse(Request.QueryString["supportId"], out supportId) && supportId != -1)
            //{
            if (supportId.HasValue)
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase postedFile1 = Request.Files["image1"];
                    HttpPostedFileBase postedFile2 = Request.Files["image2"];
                    HttpPostedFileBase postedFile3 = Request.Files["image3"];

                    string image1 = "";
                    if (postedFile1 != null && postedFile1.ContentLength > 50)
                    {
                        image1 = "image1" + Path.GetExtension(postedFile1.FileName);
                    }

                    string image2 = "";
                    if (postedFile2 != null && postedFile2.ContentLength > 50)
                    {
                        image2 = "image2" + Path.GetExtension(postedFile2.FileName);
                    }

                    string image3 = "";
                    if (postedFile3 != null && postedFile3.ContentLength > 50)
                    {
                        image3 = "image3" + Path.GetExtension(postedFile3.FileName);
                    }

                    SupportDetail detail = new SupportDetail()
                    {
                        UserId = AuthHelper.GetUserId(),
                        CreatedDate = DateTime.Now,
                        Content = model.SupportForm.Content,
                        SupportId = supportId.Value,
                        Role = false,
                        Image1 = image1,
                        Image2 = image2,
                        Image3 = image3
                    };

                    int detailID = Factory.SupportRepository.InsertSupportDetail(detail);

                    if (detailID > 0)
                    {
                        Factory.SupportRepository.UpdateSupportStatus(supportId.Value, Enums.SupportStatus.NotResolve);

                        if (postedFile1 != null && postedFile1.ContentLength > 50)
                        {
                            Enums.UploadStatus status = UploadProvider.Upload(detailID, postedFile1, FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, image1);
                        }

                        if (postedFile2 != null && postedFile2.ContentLength > 50)
                        {
                            Enums.UploadStatus status = UploadProvider.Upload(detailID, postedFile2, FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, image2);
                        }

                        if (postedFile1 != null && postedFile1.ContentLength > 50)
                        {
                            Enums.UploadStatus status = UploadProvider.Upload(detailID, postedFile1, FolderPathConstansts.SUPPORT_DETAIL_FOLDER_PATH, image3);
                        }

                        model.Message = "Phản hồi thành công";
                        model.IsValid = true;
                    }
                    else
                    {
                        model.Message = "Phản hồi thất bại";
                    }
                }
                else
                {
                    model.Message = Helper.GetErrorsOfModelState(ModelState);
                }

                Support support = Factory.SupportRepository.GetSupportById(AuthHelper.GetUserId(), supportId.Value);
                if (support != null)
                {
                    model.Support = support;
                    model.SupportDetailList = Factory.SupportRepository.GetSupportDetailList(AuthHelper.GetUserId(), supportId.Value);

                    if (support.Status == (int)Enums.SupportStatus.Resolved)
                    {
                        model.IsResolved = true;
                    }
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        #endregion

    }
}