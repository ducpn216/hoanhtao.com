using AutoMapper;
using Core;
using Core.API;
using Core.API.GameApiRequests;
using Core.API.GameApiResponses;
using DAL.DAOs;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;
using Web.BackEnd.Models;
using Core.GameAPI.Requests;
using Core.GameAPI.Responses;
using Core.Providers;
using DAL.Models;
using Web.FrontEnd.App_Code.Controllers;
using Web.BackEnd.App_Code.Attributes;

namespace Web.BackEnd.Controllers
{
    [CustomAuthorize(ForbiddenGroupId = new[] { 2, 3 })]
    public class GameController : BackEndController
    {
        GameAPI gameAPI = new GameAPI();
        ServerRepository serverRepository = new ServerRepository();
        UserRepository userRepository = new UserRepository();

        const string CAPTCHA_MESSAGE = "Mã xác nhận không hợp lệ";

        public GameController()
        {
            Mapper.CreateMap<ServerController, ServerDAO>();
            Mapper.CreateMap<ServerDAO, ServerController>();
        }

        #region Role List API
        public async Task<JsonResult> GetRoleListApi(string accountName, int? serverId)
        {
            List<RoleListResponse> roleListResponse = new List<RoleListResponse>();

            if (!string.IsNullOrWhiteSpace(accountName) && serverId.HasValue && serverId.Value > 0)
            {
                int accountId = userRepository.GetAccountId(accountName);
                if (accountId > 0)
                {
                    RoleListRequest roleListRequest = new RoleListRequest()
                    {
                        AccountId = accountId,
                        ServerId = serverId.Value,
                        Page = 1,
                        PageSize = 3
                    };

                    roleListResponse = await gameAPI.GetRoleList(roleListRequest);
                }
            }

            return Json(roleListResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Role List
        [HttpGet]
        public async Task<ActionResult> RoleList(string accountName, int? serverId)
        {
            RoleListPageModel roleListPageModel = new RoleListPageModel()
            {
                ServerList = GetServerList(serverId),
                SelectedServerId = serverId.HasValue ? serverId.Value : 0
            };

            if (!string.IsNullOrWhiteSpace(accountName) && serverId.HasValue && serverId.Value > 0)
            {
                int accountId = userRepository.GetAccountId(accountName);
                if (accountId > 0)
                {
                    RoleListRequest roleListRequest = new RoleListRequest()
                    {
                        AccountId = accountId,
                        ServerId = serverId.Value,
                        Page = 1,
                        PageSize = 3
                    };

                    List<RoleListResponse> roleListResponse = await gameAPI.GetRoleList(roleListRequest);
                    roleListPageModel.RoleDataList = roleListResponse;
                }
            }

            return View(roleListPageModel);
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> OnlineNumber(int? serverId)
        {
            OnlineNumberPageModel onlineNumberPageModel = new OnlineNumberPageModel()
            {
                ServerList = GetServerList(serverId),
                SelectedServerId = serverId.HasValue ? serverId.Value : 0
            };

            if (serverId.HasValue && serverId.Value > 0)
            {
                OnlineNumberResponse onlineNumberResponse = await gameAPI.GetOnlineNumber(serverId.Value);
                onlineNumberPageModel.OnlineNumber = onlineNumberResponse.info.Num;
            }

            return View(onlineNumberPageModel);
        }

        #region LockAccount

        string CheckLockAccountValid(LockAccountPageModel lockAccountPageModel)
        {
            string message = "";

            if (!CommonProvider.FunctionProvider.IsValidNumeric(lockAccountPageModel.Time.ToString()) || lockAccountPageModel.Time <= 0)
            {
                message = "Thời gian không hợp lệ<br/>";
            }

            //if (isValid.HasValue && !isValid.Value)
            //{
            //    message = "Mã xác nhận không hợp lệ";
            //}

            return message;
        }

        [HttpGet]
        public ActionResult LockAccount()
        {
            LockAccountPageModel lockAccountPageModel = new LockAccountPageModel()
            {
                ServerList = GetServerList(null)
            };

            return View(lockAccountPageModel);
        }

        [HttpPost]
        public async Task<JsonResult> LockAccount(LockAccountPageModel lockAccountPageModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckLockAccountValid(lockAccountPageModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                int accountId = userRepository.GetAccountId(lockAccountPageModel.AccountName);
                if (accountId > 0)
                {
                    LockAccountRequest lockAccountRequest = new LockAccountRequest()
                    {
                        ServerId = lockAccountPageModel.SelectedServerId,
                        RoleId = lockAccountPageModel.SelectedRoleId,
                        LockTime = lockAccountPageModel.Time,
                        Reason = lockAccountPageModel.Reason
                    };

                    LockAccountResponse lockAccountResponse = await gameAPI.LockAccount(lockAccountRequest);
                    if (lockAccountResponse.result == 1)
                    {
                        messageModel.Message = "Khóa thành công";
                        messageModel.Flag = true;
                    }
                    else
                    {
                        messageModel.Message = "Khóa thất bại";
                        messageModel.Flag = false;
                    }
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add Exp
        [HttpGet]
        public ActionResult AddExp()
        {
            AddExpModel addExpModel = new AddExpModel()
            {
                ServerList = GetServerList(null)
            };

            return View(addExpModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddExp(AddExpModel model)
        {
            string message = "";
            short status = 0;

            if (ModelState.IsValid)
            {
                int accountId = userRepository.GetAccountId(model.AccountName);
                if (accountId > 0)
                {
                    AddExpRequest addExpRequest = new AddExpRequest()
                    {
                        UserId = 1,
                        ServerId = model.SelectedServerId,
                        RoleId = model.SelectedRoleId,
                        Exp = model.Exp
                    };

                    AddExpResponse addExpResponse = await gameAPI.AddExp(addExpRequest);

                    if (addExpResponse != null && addExpResponse.result == 1)
                    {
                        message = "Thêm kinh nghiệm thành công";
                        status = 1;
                        TempData["Message"] = BackEndUtilities.SetMessage(message, Enums.ErrorCode.Success);
                    }
                    else
                    {
                        message = "Thêm kinh nghiệm thất bại";
                        TempData["Message"] = BackEndUtilities.SetMessage(message, Enums.ErrorCode.Error);
                    }

                    BackEndUtilities.WriteLog((int)Enums.BackEndFunction.AddExp, message, status);
                }
            }
            else
            {
                TempData["Message"] = BackEndUtilities.GetErrorsOfModelState(ModelState);
            }

            return View(model);
        }
        #endregion

        #region Ban Chat
        string CheckBanChatValid(BanChatModel banChatModel)
        {
            string message = "";

            if (string.IsNullOrEmpty(banChatModel.AccountName))
            {
                message += "Nhập tài khoản<br/>";
            }

            if (banChatModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            if (banChatModel.SelectedRoleId <= 0)
            {
                message += "Chọn nhân vật<br/>";
            }

            if (banChatModel.Time <= 0)
            {
                message += "Thời gian không hợp lệ<br/>";
            }

            message += banChatModel.CheckCaptcha();

            return message;
        }

        [HttpGet]
        public ActionResult BanChat()
        {
            BanChatModel banChatModel = new BanChatModel()
            {
                ServerList = GetServerList(null)
            };

            return View(banChatModel);
        }

        [HttpPost]
        public async Task<JsonResult> BanChat(BanChatModel banChatModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckBanChatValid(banChatModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                int accountId = userRepository.GetAccountId(banChatModel.AccountName);
                if (accountId > 0)
                {
                    BanChatRequest banChatRequest = new BanChatRequest()
                    {
                        ServerId = banChatModel.SelectedServerId,
                        RoleId = banChatModel.SelectedRoleId,
                        Seconds = banChatModel.Time
                    };

                    BanChatResponse banChatResponse = await gameAPI.BanChat(banChatRequest);

                    if (banChatResponse != null && banChatResponse.result == 1)
                    {
                        messageModel.Message = "Cấm chat thành công";
                        messageModel.Flag = true;
                    }
                    else
                    {
                        messageModel.Message = "Cấm chat thất bại";
                        messageModel.Flag = false;
                    }
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Kick Offline
        string CheckKickOfflineValid(KickOfflineModel kickOfflineModel)
        {
            string message = "";

            if (string.IsNullOrEmpty(kickOfflineModel.AccountName))
            {
                message += "Nhập tài khoản<br/>";
            }

            if (kickOfflineModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            if (kickOfflineModel.SelectedRoleId <= 0)
            {
                message += "Chọn nhân vật<br/>";
            }

            message += kickOfflineModel.CheckCaptcha();

            return message;
        }

        [HttpGet]
        public ActionResult KickOffline()
        {
            KickOfflineModel lockAccountPageModel = new KickOfflineModel()
            {
                ServerList = GetServerList(null)
            };

            return View(lockAccountPageModel);
        }

        [HttpPost]
        public async Task<ActionResult> KickOffline(KickOfflineModel kickOfflineModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckKickOfflineValid(kickOfflineModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                KickOfflineRequest kickOfflineRequest = new KickOfflineRequest()
                {
                    UserId = 1,
                    ServerId = kickOfflineModel.SelectedServerId,
                    RoleId = kickOfflineModel.SelectedRoleId
                };

                KickOfflineResponse kickOfflineResponse = await gameAPI.KickOffline(kickOfflineRequest);
                if (kickOfflineResponse != null && kickOfflineResponse.result == 1)
                {
                    messageModel.Message = "Kick thành công";
                    messageModel.Flag = true;
                }
                else
                {
                    messageModel.Message = "Kick thất bại";
                    messageModel.Flag = false;
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Char Detail
        [HttpGet]
        public ActionResult CharDetail()
        {
            CharDetailModel charDetailModel = new CharDetailModel()
            {
                ServerList = GetServerList(null)
            };

            return View(charDetailModel);
        }

        [HttpPost]
        public async Task<ActionResult> CharDetail(CharDetailModel charDetailModel)
        {
            charDetailModel.ServerList = GetServerList(charDetailModel.SelectedServerId);

            if (!string.IsNullOrWhiteSpace(charDetailModel.AccountName) && charDetailModel.SelectedServerId > 0
                && charDetailModel.SelectedRoleId > 0)
            {
                int accountId = userRepository.GetAccountId(charDetailModel.AccountName);
                CharDetailRequest charDetailRequest = new CharDetailRequest()
                {
                    UserId = 1,
                    RoleId = charDetailModel.SelectedRoleId,
                    ServerId = charDetailModel.SelectedServerId,
                    Type = ApiEnums.CheckCharDetailType.All,
                    Page = 1,
                    PageSize = 1
                };

                charDetailModel.CharDetailResponse = await gameAPI.GetCharDetail(charDetailRequest);
                charDetailModel.RoleList = await GetRoleList(accountId, charDetailModel.SelectedServerId, charDetailModel.SelectedRoleId);
                charDetailModel.WebKNB = Factory.UserRepository.GetGold(accountId);
                charDetailModel.GameKNB = Factory.UserRepository.GetKnbInGame(accountId);
            }

            return View(charDetailModel);
        }

        #endregion

        #region Delete Item
        string CheckDeleteItemValid(DeleteItemModel deleteItemModel)
        {
            string message = "";

            if (string.IsNullOrEmpty(deleteItemModel.AccountName))
            {
                message += "Nhập tài khoản<br/>";
            }

            if (deleteItemModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            if (deleteItemModel.SelectedRoleId <= 0)
            {
                message += "Chọn nhân vật<br/>";
            }

            if (string.IsNullOrEmpty(deleteItemModel.ItemId))
            {
                message += "Nhập ItemID<br/>";
            }

            message += deleteItemModel.CheckCaptcha();

            return message;
        }

        [HttpGet]
        public ActionResult DeleteItem()
        {
            DeleteItemModel deleteItemModel = new DeleteItemModel()
            {
                ServerList = GetServerList(null)
            };

            return View(deleteItemModel);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteItem(DeleteItemModel deleteItemModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckDeleteItemValid(deleteItemModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                DeleteItemRequest deleteItemRequest = new DeleteItemRequest()
               {
                   UserId = 1,
                   ServerId = deleteItemModel.SelectedServerId,
                   RoleId = deleteItemModel.SelectedRoleId,
                   ItemId = deleteItemModel.ItemId
               };

                DeleteItemResponse deleteItemResponse = await gameAPI.DeleteItem(deleteItemRequest);
                if (deleteItemResponse != null && deleteItemResponse.result == 1)
                {
                    messageModel.Message = "Xóa item thành công";
                    messageModel.Flag = true;
                }
                else
                {
                    messageModel.Message = "Xóa item thất bại";
                    messageModel.Flag = false;
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Modify Item
        //string CheckModifyItemValid(ModifyItemModel modifyItemModel)
        //{
        //    string message = "";

        //    if (modifyItemModel.SelectedServerId <= 0)
        //    {
        //        message += "Chọn server<br/>";
        //    }

        //    if (modifyItemModel.SelectedSellStatus <= 0)
        //    {
        //        message += "Chọn trạng thái<br/>";
        //    }

        //    if (string.IsNullOrEmpty(modifyItemModel.ItemId))
        //    {
        //        message += "Nhập Item ID<br/>";
        //    }

        //    message += modifyItemModel.CheckCaptcha();

        //    return message;
        //}

        //[HttpGet]
        //public ActionResult ModifyItem()
        //{
        //    ModifyItemModel modifyItemModel = new ModifyItemModel()
        //    {
        //        ServerList = GetServerList(null)
        //    };

        //    return View(modifyItemModel);
        //}

        //[HttpPost]
        //public async Task<ActionResult> ModifyItem(ModifyItemModel modifyItemModel)
        //{
        //    MessageModel messageModel = new MessageModel();

        //    string message = CheckModifyItemValid(modifyItemModel);

        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        messageModel.Message = message;
        //        messageModel.Flag = false;
        //    }
        //    else
        //    {
        //        DetailModifyItemRequest detailModifyItemRequest = new DetailModifyItemRequest()
        //        {
        //            ItemType = modifyItemModel.ItemTypeId,
        //            Price = modifyItemModel.Price,
        //            Discount = modifyItemModel.Discount,
        //            SellStatus = modifyItemModel.SelectedSellStatus
        //        };

        //        ModifyMallRequest modifyItemRequest = new ModifyMallRequest()
        //        {
        //            ServerId = modifyItemModel.SelectedServerId,
        //            Data = new List<DetailModifyItemRequest>() 
        //            { 
        //                detailModifyItemRequest
        //            }
        //        };

        //        ModifyMallResponse kickOfflineResponse = await gameAPI.ModifyItem(modifyItemRequest);
        //        if (kickOfflineResponse != null && kickOfflineResponse.result == 1)
        //        {
        //            messageModel.Message = "Điều chỉnh giá thành công";
        //            messageModel.Flag = true;
        //        }
        //        else
        //        {
        //            messageModel.Message = "Điều chỉnh giá thất bại";
        //            messageModel.Flag = false;
        //        }
        //    }

        //    return Json(messageModel, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Send Mail

        string CheckSendMailValid(SendMailModel sendMailModel)
        {
            string message = "";

            if (string.IsNullOrEmpty(sendMailModel.AccountName))
            {
                message += "Nhập tài khoản<br/>";
            }

            if (sendMailModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            if (sendMailModel.SelectedRoleId <= 0)
            {
                message += "Chọn nhân vật<br/>";
            }

            if (string.IsNullOrEmpty(sendMailModel.Title))
            {
                message += "Nhập tiêu đề<br/>";
            }

            if (string.IsNullOrEmpty(sendMailModel.Content))
            {
                message += "Nhập nội dung<br/>";
            }

            if (!string.IsNullOrEmpty(sendMailModel.ItemId1) && sendMailModel.NumberOfItem1 <= 0)
            {
                message += "Số lượng item 1 không hợp lệ<br/>";
            }

            if (!string.IsNullOrEmpty(sendMailModel.ItemId2) && sendMailModel.NumberOfItem2 <= 0)
            {
                message += "Số lượng item 2 không hợp lệ<br/>";
            }

            if (!string.IsNullOrEmpty(sendMailModel.ItemId3) && sendMailModel.NumberOfItem3 <= 0)
            {
                message += "Số lượng item 3 không hợp lệ<br/>";
            }

            if (!string.IsNullOrEmpty(sendMailModel.ItemId4) && sendMailModel.NumberOfItem4 <= 0)
            {
                message += "Số lượng item 4 không hợp lệ<br/>";
            }

            if (!string.IsNullOrEmpty(sendMailModel.ItemId5) && sendMailModel.NumberOfItem5 <= 0)
            {
                message += "Số lượng item 5 không hợp lệ<br/>";
            }
            if (sendMailModel.Gold <= 0 && sendMailModel.BindGold <= 0 && sendMailModel.Money <= 0
                && string.IsNullOrEmpty(sendMailModel.ItemId1)
                && string.IsNullOrEmpty(sendMailModel.ItemId2)
                && string.IsNullOrEmpty(sendMailModel.ItemId3)
                && string.IsNullOrEmpty(sendMailModel.ItemId4)
                && string.IsNullOrEmpty(sendMailModel.ItemId5))
            {
                message += "Phải chọn ít nhất 1 chức năng để thực hiện<br/>";
            }

            message += sendMailModel.CheckCaptcha();

            return message;
        }

        [HttpGet]
        public ActionResult SendMail()
        {
            SendMailModel lockAccountPageModel = new SendMailModel()
            {
                ServerList = GetServerList(null)
            };

            return View(lockAccountPageModel);
        }

        [HttpPost]
        public async Task<JsonResult> SendMail(SendMailModel sendMailModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckSendMailValid(sendMailModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                int accountId = userRepository.GetAccountId(sendMailModel.AccountName);
                if (accountId > 0)
                {
                    SendMailRequest sendMailRequest = new SendMailRequest()
                    {
                        BindGold = sendMailModel.BindGold,
                        Content = sendMailModel.Content,
                        Gold = sendMailModel.Gold,                
                        Money = sendMailModel.Money,
                        RoleId = sendMailModel.SelectedRoleId,
                        SendAll = ApiEnums.SendAll.None,
                        SenderId = sendMailModel.SenderId,
                        SenderName = "Hoanhtao.com",
                        ServerId = sendMailModel.SelectedServerId,
                        Title = sendMailModel.Title,
                        UserId = 1,
                        ItemId1 = sendMailModel.ItemId1,
                        NumberOfItem1 = sendMailModel.NumberOfItem1,
                        ItemId2 = sendMailModel.ItemId2,
                        NumberOfItem2 = sendMailModel.NumberOfItem2,
                        ItemId3 = sendMailModel.ItemId3,
                        NumberOfItem3 = sendMailModel.NumberOfItem3,
                        ItemId4 = sendMailModel.ItemId4,
                        NumberOfItem4 = sendMailModel.NumberOfItem4,
                        ItemId5 = sendMailModel.ItemId5,
                        NumberOfItem5 = sendMailModel.NumberOfItem5,
                    };

                    string logs = "";
                    short status = 1;

                    SendMailResponse sendMailResponse = await gameAPI.SendMail(sendMailRequest);
                    if (sendMailResponse != null && sendMailResponse.result == 1)
                    {
                        messageModel.Message = "Gửi thư thành công";
                        messageModel.Flag = true;
                    }
                    else
                    {
                        messageModel.Message = "Gửi thư thất bại";
                        messageModel.Flag = false;
                        status = 0;
                    }

                    logs +=  messageModel.Message + " | Account:" + sendMailModel.AccountName +
                                " | ServerId :" + sendMailModel.SelectedServerId +
                                " | KNB :" + sendMailModel.Gold +
                                " | Bind Gold :" + sendMailModel.BindGold +
                                " | Money :" + sendMailModel.Money +
                                " | Item1 :" + sendMailModel.ItemId1 +
                                " | Number1 :" + sendMailModel.NumberOfItem1 +
                                " | Item2 :" + sendMailModel.ItemId2 +
                                " | Number2 :" + sendMailModel.NumberOfItem2 +
                                " | Item3 :" + sendMailModel.ItemId3 +
                                " | Number3 :" + sendMailModel.NumberOfItem3 +
                                " | Item4 :" + sendMailModel.ItemId4 +
                                " | Number4 :" + sendMailModel.NumberOfItem4 +
                                " | Item5 :" + sendMailModel.ItemId5 +
                                " | Number5 :" + sendMailModel.NumberOfItem5;

                    BackEndUtilities.WriteLog((int)Enums.BackEndFunction.SendMail, logs, status);
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Jump Map
        string CheckJumpMapValid(JumpMapModel jumpMapModel)
        {
            string message = "";

            if (string.IsNullOrEmpty(jumpMapModel.AccountName))
            {
                message += "Nhập tài khoản<br/>";
            }

            if (jumpMapModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            if (jumpMapModel.SelectedRoleId <= 0)
            {
                message += "Chọn nhân vật<br/>";
            }

            if (string.IsNullOrEmpty(jumpMapModel.MapId))
            {
                message += "Nhập Map ID<br/>";
            }

            if (string.IsNullOrEmpty(jumpMapModel.X))
            {
                message += "Nhập tọa độ X<br/>";
            }
            else if (!CommonProvider.FunctionProvider.IsValidNumeric(jumpMapModel.X))
            {
                message += "Tọa độ X không hợp lệ<br/>";
            }

            if (string.IsNullOrEmpty(jumpMapModel.Y))
            {
                message += "Nhập tọa độ Y<br/>";
            }
            else if (!CommonProvider.FunctionProvider.IsValidNumeric(jumpMapModel.X))
            {
                message += "Tọa độ Y không hợp lệ<br/>";
            }

            message += jumpMapModel.CheckCaptcha();

            return message;
        }

        [HttpGet]
        public ActionResult JumpMap()
        {
            JumpMapModel jumpMapModel = new JumpMapModel()
            {
                ServerList = GetServerList(null)
            };

            return View(jumpMapModel);
        }

        [HttpPost]
        public async Task<JsonResult> JumpMap(JumpMapModel jumpMapModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckJumpMapValid(jumpMapModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                int accountId = userRepository.GetAccountId(jumpMapModel.AccountName);
                if (accountId > 0)
                {
                    JumpMapRequest jumpMapRequest = new JumpMapRequest()
                    {
                        UserId = 1,
                        ServerId = jumpMapModel.SelectedServerId,
                        RoleId = jumpMapModel.SelectedRoleId,
                        MapId = jumpMapModel.MapId,
                        X = jumpMapModel.X,
                        Y = jumpMapModel.Y
                    };

                    JumpMapResponse jumpMapResponse = await gameAPI.JumpMap(jumpMapRequest);

                    if (jumpMapResponse != null && jumpMapResponse.result == 1)
                    {
                        messageModel.Message = "Nhảy map thành công";
                        messageModel.Flag = true;
                    }
                    else
                    {
                        messageModel.Message = "Nhảy map thất bại";
                        messageModel.Flag = false;
                    }
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ModifyMall
        string CheckModifyMallValid(ModifyMallModel modifyMallModel)
        {
            string message = "";

            if (modifyMallModel.SelectedServerId <= 0)
            {
                message += "Chọn server<br/>";
            }

            message += modifyMallModel.CheckCaptcha();

            return message;
        }

        [HttpGet]
        public ActionResult ModifyMall()
        {
            JumpMapModel jumpMapModel = new JumpMapModel()
            {
                ServerList = GetServerList(null)
            };

            return View(jumpMapModel);
        }

        [HttpPost]
        public async Task<JsonResult> ModifyMall(ModifyMallModel modifyMallModel)
        {
            MessageModel messageModel = new MessageModel();

            string message = CheckModifyMallValid(modifyMallModel);

            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Flag = false;
            }
            else
            {
                ModifyMallRequest modifyMallRequest = new ModifyMallRequest()
                {
                    ServerId = modifyMallModel.SelectedServerId,
                    Data = new List<DetailModifyMallRequest>()
                    {
                        new DetailModifyMallRequest(){
                            ItemId = modifyMallModel.ItemId,
                            Price = modifyMallModel.Price,
                            Discount = modifyMallModel.Discount,
                            SellStatus = modifyMallModel.SelectedSellStatus,
                            ItemType = 1
}
                    }
                };

                ModifyMallResponse modifyMallResponse = await gameAPI.ModifyMall(modifyMallRequest);

                if (modifyMallResponse != null && modifyMallResponse.result == 1)
                {
                    messageModel.Message = "Điều chỉnh thành công";
                    messageModel.Flag = true;
                }
                else
                {
                    messageModel.Message = "Điều chỉnh thất bại";
                    messageModel.Flag = false;
                }
            }

            return Json(messageModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Send Notice
        [HttpGet]
        public ActionResult SendNotice()
        {
            SendNoticeModel addExpModel = new SendNoticeModel()
            {
                ServerList = BackEndUtilities.GetServerList(null)
            };

            return View(addExpModel);
        }

        [HttpPost]
        public async Task<ActionResult> SendNotice(SendNoticeModel model)
        {
            string message = "";
            short status = 0;

            if (ModelState.IsValid)
            {
                SendNoticeRequest request = new SendNoticeRequest()
                {
                    UserId = 1,
                    ServerId = model.SelectedServerId,
                    Notice = model.Notice,
                    Link = model.Link
                };

                SendNoticeResponse response = await gameAPI.SendNotice(request);

                if (response != null && response.result == 1)
                {
                    message = "Gửi thông báo thành công";
                    status = 1;
                    TempData["Message"] = BackEndUtilities.SetMessage(message, Enums.ErrorCode.Success);
                }
                else
                {
                    message = "Gửi thông báo thất bại";
                    TempData["Message"] = BackEndUtilities.SetMessage(message, Enums.ErrorCode.Error);
                }

                BackEndUtilities.WriteLog((int)Enums.BackEndFunction.AddExp, message, status);
            }
            else
            {
                TempData["Message"] = BackEndUtilities.GetErrorsOfModelState(ModelState);
            }

            model.ServerList = BackEndUtilities.GetServerList(null);

            return View(model);
        }
        #endregion

        public void ShowCaptchaMessage()
        {
            TempData["Message"] = BackEndUtilities.SetMessage(CAPTCHA_MESSAGE, Enums.ErrorCode.Error);
        }

        public List<SelectListItem> GetServerList(int? selectedServerId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<Server> serverDaoList = serverRepository.GetList();

            foreach (var server in serverDaoList)
            {
                bool selected = false;

                if (selectedServerId.HasValue && server.ServerId == selectedServerId.Value)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = server.ServerName, Value = server.ServerId.ToString(), Selected = selected });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Chọn server", Value = "" });

            return itemList;
        }

        public async Task<List<SelectListItem>> GetRoleList(int accountId, int? serverId, int? roleId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<RoleListResponse> roleListResponse = new List<RoleListResponse>();

            if (accountId > 0 && serverId.HasValue && serverId.Value > 0)
            {
                RoleListRequest roleListRequest = new RoleListRequest()
                {
                    AccountId = accountId,
                    ServerId = serverId.Value,
                    Page = 1,
                    PageSize = 3
                };

                roleListResponse = await gameAPI.GetRoleList(roleListRequest);
                if (roleListResponse != null)
                {
                    foreach (var role in roleListResponse)
                    {
                        bool selected = false;

                        if (roleId.HasValue && role.dwRoleID == roleId.Value.ToString())
                        {
                            selected = true;
                        }

                        itemList.Add(new SelectListItem() { Text = role.szRoleName, Value = role.dwRoleID.ToString(), Selected = selected });
                    }
                }
            }

            return itemList;
        }

        public List<SelectListItem> GetSellStatus(Enums.SellStatus sellStatus)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            foreach (Enums.SellStatus status in Enum.GetValues(typeof(Enums.SellStatus)))
            {
                bool selected = false;

                if (status == sellStatus)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = Core.Utilities.GetSellStatus(status), Value = ((int)sellStatus).ToString(), Selected = selected });
            }

            return itemList;
        }
    }
}