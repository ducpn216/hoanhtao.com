using Core;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;
using Web.BackEnd.Models;
using Web.BackEnd.Models.Marketing;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{
    public class MarketingController : BackEndController
    {
        const int PAGE_SIZE = 30;
        const int PAGE_COUNT = 5;

        PidRepository pidRepository = new PidRepository();

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            PidListModel model = new PidListModel();

            int total = 0;
            model.PidList = pidRepository.GetList(page, PAGE_SIZE, ref total);

            //ViewBag["Paging"]

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid? pid)
        {
            PidFormModel model = new PidFormModel();
            if (pid.HasValue)
            {
                DAL.Models.Pid pidModel = pidRepository.GetById(pid.Value);
                if (pidModel != null)
                {
                    model.Pid = pidModel.Pid1;
                    model.Link = pidModel.Link;
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PidFormModel model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                DAL.Models.Pid pidModel = new DAL.Models.Pid()
                {
                    Title = model.Title,
                    Link = model.Link
                };

                if (model.Pid == null || model.Pid == Guid.Empty)
                {
                    pidModel.Pid1 = Guid.NewGuid();
                    pidModel.CreatedBy = AuthHelper.GetUserId();
                    pidModel.CreatedDate = DateTime.Now;

                    Guid pid = pidRepository.Insert(pidModel);
                    if (pid != null && pid != Guid.Empty)
                    {
                        message = BackEndUtilities.SetMessage("Thêm thành công", Enums.ErrorCode.Success);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        message = BackEndUtilities.SetMessage("Thêm thất bại", Enums.ErrorCode.Error);
                    }
                }
                else
                {
                    pidModel.Pid1 = model.Pid;

                    if (pidRepository.Update(pidModel))
                    {
                        message = BackEndUtilities.SetMessage("Cập nhật thành công", Enums.ErrorCode.Success);
                    }
                    else
                    {
                        message = BackEndUtilities.SetMessage("Cập nhật thất bại", Enums.ErrorCode.Error);
                    }
                }

            }
            else
            {
                message = BackEndUtilities.GetErrorsOfModelState(ModelState);
            }

            TempData["Message"] = message;

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(Guid? pid)
        {
            if (pid.HasValue && pid.Value != Guid.Empty)
            {
                if (pidRepository.Delete(pid.Value))
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Xóa thành công", Enums.ErrorCode.Success);

                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Xóa thất bại", Enums.ErrorCode.Error);
                }
            }

            return RedirectToAction("Index");
        }
    }
}