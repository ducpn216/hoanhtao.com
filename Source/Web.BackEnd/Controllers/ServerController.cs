using AutoMapper;
using Core;
using Core.Providers;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
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
    public class ServerController : BackEndController
    {
        ServerRepository serverRepository = new ServerRepository();

        const int POST_PAGE_COUNT = 5;
        const int POST_PAGE_SIZE = 10;

        public ServerController()
        {
            Mapper.CreateMap<Server, ServerModel>();
            Mapper.CreateMap<ServerModel, Server>();
        }

        [HttpGet]
        public ActionResult Index(short? status = (short)Enums.Status.Active, int page = 1)
        {
            ServerListModel serverListModel = new ServerListModel()
            {
                StatusList = BackEndUtilities.GetStatusList()
            };

            int total = 0;
            bool isActive = status == (int)Enums.Status.Active ? true : false;

            List<Server> serverList = serverRepository.GetList(isActive, true, page, POST_PAGE_SIZE, ref total);

            if (serverList != null && serverList.Count > 0)
            {
                foreach (var server in serverList)
                {
                    serverListModel.ServerList.Add(Mapper.Map<Server, ServerModel>(server));
                }

                string queryString = String.Format("?status={0}&", status);

                ViewBag.Paging = CommonProvider.FunctionProvider.DrawPaging2(page, POST_PAGE_SIZE, POST_PAGE_COUNT,
                    total, 0, queryString, "Sau", "Trước");
            }

            serverListModel.SelectedStatus = status.Value;

            return View(serverListModel);
        }

        [HttpGet]
        public ActionResult Edit(int? serverId)
        {
            ServerFormModel serverFormModel = new ServerFormModel()
            {
                StatusList = BackEndUtilities.GetStatusList()
            };

            if (serverId.HasValue)
            {
                DAL.Models.Server server = serverRepository.GetById(serverId.Value);
                if (server != null)
                {
                    //serverFormModel.SelectedStatus = server.Status;
                    serverFormModel.ServerModel = Mapper.Map<Server, ServerModel>(server);
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

            return View(serverFormModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ServerFormModel serverFormModel, int? serverId)
        {
            if (serverFormModel != null)
            {
                string validateMessage = CheckEditValid(serverFormModel);
                if (string.IsNullOrWhiteSpace(validateMessage))
                {
                    ServerModel serverModel = serverFormModel.ServerModel;

                    Server server = Mapper.Map<ServerModel, Server>(serverModel);
                    server.IsNew = serverModel.IsNew;
                    server.IsActive = serverModel.IsActive;
                    //server.Status = serverFormModel.SelectedStatus;

                    //serverModel.IsNew = server.IsNew;

                    if (serverId.HasValue && serverId.Value > 0)
                    {
                        if (serverRepository.Update(server))
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
                        server.ServerId = serverRepository.Insert(server);
                        if (server.ServerId > 0)
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thành công", Enums.ErrorCode.Success);
                            return RedirectToAction("Index", new { status = serverFormModel.SelectedStatus });
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thất bại", Enums.ErrorCode.Error);
                        }
                    }
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage(validateMessage, Enums.ErrorCode.Error);
                }

                serverFormModel.StatusList = BackEndUtilities.GetStatusList(serverFormModel.SelectedStatus);
            }


            return View(serverFormModel);
        }

        [HttpGet]
        public ActionResult Delete(int? serverId, int? status)
        {
            if (serverId.HasValue)
            {
                if (serverRepository.Delete(serverId.Value))
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

        string CheckEditValid(ServerFormModel serverFormModel)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(serverFormModel.ServerModel.ServerName))
            {
                message = "Nhập tên server<br/>";
            }

            return message;
        }
    }
}