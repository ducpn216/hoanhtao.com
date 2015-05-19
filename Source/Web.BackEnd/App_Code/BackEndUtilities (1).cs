using Core;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.App_Code
{
    public class BackEndUtilities
    {
        public static string SetMessage(string message, Enums.ErrorCode status = Enums.ErrorCode.Success)
        {
            string value = "";
            string className = "";

            switch (status)
            {
                case Enums.ErrorCode.Success:
                    className = "alert-success";
                    break;
                case Enums.ErrorCode.Error:
                    className = "alert-block alert-danger";
                    break;
                case Enums.ErrorCode.Warning:
                    className = "alert-warning";
                    break;
                default:
                    break;
            }

            value = string.Format("<div class=\"alert {0} fade in\">" +
                                    "<button data-dismiss=\"alert\" class=\"close close-sm\" type=\"button\">" +
                                      "<i class=\"icon-remove\"></i>" +
                                    "</button>" +
                                    "{1}" +
                                  "</div>", className, message);

            return value;
        }

        public static List<SelectListItem> GetCategoryList(int? selectedCategoryId = null)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> categoryList = categoryRepository.GetList();

            foreach (var category in categoryList)
            {
                bool selected = false;

                if (category.CategoryId == selectedCategoryId)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = category.CategoryName, Value = category.CategoryId.ToString(), Selected = selected });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Danh mục chính", Value = "0" });

            return itemList;
        }

        //public List<SelectListItem> GetPostTypeList(int? selectedPostTypeId = null)
        //{
        //    List<SelectListItem> itemList = new List<SelectListItem>();
        //    foreach (Enums.PostType postType in Enum.GetValues(typeof(Enums.PostType)))
        //    {
        //        bool selected = false;
        //        int postTypeId = (int)postType;

        //        if (postTypeId == selectedPostTypeId)
        //        {
        //            selected = true;
        //        }

        //        itemList.Add(new SelectListItem() { Text = Core.Utilities.GetPostTypeName(postType), Value = postTypeId.ToString(), Selected = selected });
        //    }

        //    itemList.Insert(0, new SelectListItem() { Text = "Chọn loại", Value = "" });

        //    return itemList;
        //}

        public static List<SelectListItem> GetPostTypeList(int? postTypeId = null)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            foreach (Enums.PostType postType in Enum.GetValues(typeof(Enums.PostType)))
            {
                bool selected = false;

                if (postTypeId.HasValue && (int)postType == postTypeId.Value)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = Utilities.GetPostTypeName((int)postType), Value = ((int)postType).ToString(), Selected = selected });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Chọn loại tin", Value = "" });

            return itemList;
        }

        public static List<SelectListItem> GetStatusList(short? selectedStatus = null)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            foreach (Enums.Status status in Enum.GetValues(typeof(Enums.Status)))
            {
                bool selected = false;

                if (selectedStatus.HasValue && (short)status == selectedStatus.Value)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = Utilities.GetStatusName(status), Value = ((short)status).ToString(), Selected = selected });
            }

            //itemList.Insert(0, new SelectListItem() { Text = "Chọn trạng thái", Value = "" });

            return itemList;
        }

        public static List<SelectListItem> GetActiveList(short? selectedStatus = null)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            foreach (Enums.Status status in Enum.GetValues(typeof(Enums.Status)))
            {
                bool selected = false;

                if (selectedStatus.HasValue && (short)status == selectedStatus.Value)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = Utilities.GetStatusName(status), Value = ((short)status).ToString(), Selected = selected });
            }

            //itemList.Insert(0, new SelectListItem() { Text = "Chọn trạng thái", Value = "" });

            return itemList;
        }
        //public static List<SelectListItem> GetStatusList(int selectedStatus)
        //{
        //    List<SelectListItem> itemList = new List<SelectListItem>();

        //    foreach (Enums.Status status in Enum.GetValues(typeof(Enums.Status)))
        //    {
        //        bool selected = false;

        //        if ((int)status == selectedStatus)
        //        {
        //            selected = true;
        //        }

        //        itemList.Add(new SelectListItem() { Text = Utilities.GetStatusName(status), Value = ((int)status).ToString(), Selected = selected });
        //    }

        //    itemList.Insert(0, new SelectListItem() { Text = "Chọn trạng thái", Value = "" });

        //    return itemList;
        //}

        public static string GetErrorsOfModelState(ModelStateDictionary modselState)
        {
            string message = "";

            var errors = modselState.Values.SelectMany(v => v.Errors);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    message += SetMessage(error.ErrorMessage, Enums.ErrorCode.Error);
                }
            }

            return message;
        }

        public static List<SelectListItem> GetServerList(int? selectedServerId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            ServerRepository serverRepository = new ServerRepository();
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

        public static List<SelectListItem> GetPidList(string selectedPidId = "")
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<Pid> pidList = Factory.PidRepository.GetList();

            foreach (var pid in pidList)
            {
                bool selected = false;

                if (pid.Pid1.ToString() == selectedPidId)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = pid.Title, Value = pid.Pid1.ToString(), Selected = selected });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Chọn pid", Value = "" });

            return itemList;
        }

        public static void WriteLog(int functionId, string description, short status = 1)
        {
            Factory.ToolLogRepository.Insert(new ToolLog()
            {
                UserId = AuthHelper.GetUserId(),
                Username = AuthHelper.GetUsername(),
                FunctionId = functionId,
                Description = description,
                Status = status,
                CreatedDate = DateTime.Now
            });
        }
    }
}