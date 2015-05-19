using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class BanChatRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int RoleId { get; set; }
        public int Seconds { get; set; }
    }
}
