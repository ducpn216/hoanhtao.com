using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Requests
{
    public class ModifyMallRequest
    {
        public int ServerId { get; set; }
        public List<DetailModifyMallRequest> Data { get; set; }
    }

    public class DetailModifyMallRequest
    {
        public int ItemType { get; set; }
        public string ItemId { get; set; }
        public int SellStatus { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
    }
}
