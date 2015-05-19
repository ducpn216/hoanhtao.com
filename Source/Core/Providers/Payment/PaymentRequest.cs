using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Payment
{
    public class PaymentRequest
    {
        public string Serial { get; set; }
        public string Pin { get; set; }
        public string Provider { get; set; }
        public string ProductId { get; set; }
    }
}
