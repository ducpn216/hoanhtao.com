using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Payment
{
    public class PaymentResponse
    {
        public int status { get; set; }
        public string msg { get; set; }
        public string trans_id { get; set; }
        public string amount { get; set; }
    }
}
