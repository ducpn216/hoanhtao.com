using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.GameApiResponses
{
    public class OnlineNumberResponse
    {
        public int result { get; set; }
        public OnlineNum info { get; set; }
    }

    public class OnlineNum
    {
        public int Num { get; set; }
    }
}
