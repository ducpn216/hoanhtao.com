using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Google.Requests
{
    public class TokenRequest
    {
        public string Scope { get; set; }
        public string State { get; set; }
        public string ApprovalPrompt { get; set; }
    }
}
