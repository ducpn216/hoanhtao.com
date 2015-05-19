using Core;
using DAL.Reponses;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.FrontEnd.App_Code
{
    public class FrontEndUtils
    {
        public static List<SelectListItem> GetServerList(int? serverId = null)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            ServerRepository serverRepository = new ServerRepository();
            List<DAL.Models.Server> serverList = serverRepository.GetList(true, true);

            foreach (DAL.Models.Server server in serverList)
            {
                bool selected = false;

                if (serverId.HasValue && (int)server.ServerId == serverId.Value)
                {
                    selected = true;
                }

                itemList.Add(new SelectListItem() { Text = server.ServerName, Value = server.ServerId.ToString(), Selected = selected });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Chọn server", Value = "" });

            return itemList;
        }

        public static List<SelectListItem> GetKNBList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            itemList.Add(new SelectListItem() { Text = "100", Value = "100" });
            itemList.Add(new SelectListItem() { Text = "200", Value = "200" });
            itemList.Add(new SelectListItem() { Text = "300", Value = "300" });
            itemList.Add(new SelectListItem() { Text = "500", Value = "500" });
            itemList.Add(new SelectListItem() { Text = "1000", Value = "1000" });
            itemList.Add(new SelectListItem() { Text = "2000", Value = "2000" });
            itemList.Add(new SelectListItem() { Text = "5000", Value = "5000" });
            itemList.Insert(0, new SelectListItem() { Text = "Chọn số kim nguyên bảo", Value = "" });

            return itemList;
        }

        public static List<SelectListItem> GetSupportStatusList(int? selectedStatus)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            itemList.Add(new SelectListItem() { Text = "Chưa xử lý", Value = ((int)Enums.SupportStatus.NotResolve).ToString() });
            itemList.Add(new SelectListItem() { Text = "Đã xử lý", Value = ((int)Enums.SupportStatus.Resolved).ToString() });

            //itemList.Insert(0, new SelectListItem() { Text = "Chọn server", Value = "" });

            return itemList;
        }

        public static List<SelectListItem> GetSupportTypeList(int? selectedStatus)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            ServerRepository serverRepository = new ServerRepository();

            List<SupportTypeResponse> list = Factory.SupportRepository.GetSupportTypeList();
            foreach (var item in list)
            {
                itemList.Add(new SelectListItem() { Text = item.SupportTypeName, Value = item.SupportTypeId.ToString() });
            }

            itemList.Insert(0, new SelectListItem() { Text = "Chọn loại hình hỗ trợ", Value = "" });

            return itemList;
        }
    }

}