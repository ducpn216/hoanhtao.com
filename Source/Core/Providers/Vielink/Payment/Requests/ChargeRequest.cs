using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Payment.Requests
{
    public class ChargeRequest
    {
        public string merchantCode { get; set; }
        public string cardtype { get; set; }
        public string cardserial { get; set; }
        public string cardcode { get; set; }
        public string signkey { get; set; }
        public string target { get; set; }
    }
}
