using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.GameApiRequests
{
    public class QueryItemRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int RoleId { get; set; }
        public ApiEnums.QueryItemType Type { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
