using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reponses
{
    public class TrackingResponse
    {
        public DateTime date { get; set; }
        public int totalmoney { get; set; }
    }

    public class TrackingHourResponse
    {
        public int date { get; set; }
        public int totalmoney { get; set; }
    }
}
