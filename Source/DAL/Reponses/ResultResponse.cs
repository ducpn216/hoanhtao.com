using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reponses
{
    public class ResultResponse
    {
        int flag = -1;

        public int Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        public string Message { get;set; }
    }
}
