using Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class SendMailRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int RoleId { get; set; }
        public ApiEnums.SendAll? SendAll { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Gold { get; set; }
        public int BindGold { get; set; }
        public int Money { get; set; }
        public string ItemId1 { get; set; }
        public int NumberOfItem1 { get; set; }
        public string ItemId2 { get; set; }
        public int NumberOfItem2 { get; set; }
        public string ItemId3 { get; set; }
        public int NumberOfItem3 { get; set; }
        public string ItemId4 { get; set; }
        public int NumberOfItem4 { get; set; }
        public string ItemId5 { get; set; }
        public int NumberOfItem5 { get; set; }
    }
}
