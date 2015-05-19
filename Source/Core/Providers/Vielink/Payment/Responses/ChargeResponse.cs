using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Vielink.Payment.Responses
{
    public class ChargeResponse
    {
        public string retCode { get; set; }
        public string retMsg { get; set; }
        public string data_transId { get; set; }
        public string data_cardValue { get; set; }
    }
}
