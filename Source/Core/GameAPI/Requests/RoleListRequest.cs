using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.GameApiRequests
{
    public class RoleListRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string AccountName { get; set; }
        public int AccountId { get; set; }
        public string LoginIP { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
