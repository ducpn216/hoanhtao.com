using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Payment.Responses
{
    public class CardResponse
    {
        //public List<CardDetailResponse> List { get; set; }
        public string CardType { get; set; }
        public string CardName { get; set; }
    }

    public class CardDetailResponse
    {
        public string CardType { get; set; }
        public string CardName { get; set; }
    }
}
