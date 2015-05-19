using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.GameApiRequests
{
    public class LockAccountRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public int RoleId { get; set; }
        public int RoleName { get; set; }
        public int LockTime { get; set; }
        public string Reason { get; set; }
    }
}
