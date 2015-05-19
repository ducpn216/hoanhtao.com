using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class KickOfflineRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int RoleId { get; set; }
    }
}
