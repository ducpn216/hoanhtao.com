using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Core.Constants
{
    public class PaymentConstants
    {
        public static readonly string IP = WebConfigurationManager.AppSettings["VieLinkPaymentIP"];
        public static readonly string CLIENT_NAME = WebConfigurationManager.AppSettings["VieLinkPaymentClientName"];
        public static readonly string CLIENT_KEY = WebConfigurationManager.AppSettings["VieLinkPaymentClientKey"];
    }
}
