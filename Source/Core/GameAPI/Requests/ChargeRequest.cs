using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class ChargeRequest
    {
        public int ServerId { get; set; }
        public int AccountId { get; set; }
        public string TransId { get; set; }
        public int GamePoint { get; set; }
    }
}
