using Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class CharDetailRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int RoleId { get; set; }
        public ApiEnums.CheckCharDetailType Type { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
