using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class SendNoticeRequest
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public string Notice { get; set; }
        public string Link { get; set; }
    }
}
