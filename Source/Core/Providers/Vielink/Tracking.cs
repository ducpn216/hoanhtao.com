using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Providers.Vielink
{
    public class Tracking
    {
        const string BASE_ADDRESS = "";
        const string PKEY = "";

        public object ReportTurnOverByDate(DateTime startDate, DateTime endDate)
        {
            string url = String.Format(BASE_ADDRESS + "/sttpaymentpartner?pkey={0}&startdate={1}&enddate={2}", 
                PKEY, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            return Helper.CallApi<object>(url);
        }

        public object ReportTurnOverByMonth(int year, int month)
        {
            string url = String.Format(BASE_ADDRESS + "/sttpaymentpartner?pkey={0}&year={1}&month={2}",
                PKEY, year, month);

            return Helper.CallApi<object>(url);
        }

        public object ReportTurnOverByHour(DateTime date)
        {
            string url = String.Format(BASE_ADDRESS + "/sttpaymentpartner?pkey={0}&date={1}",
                PKEY, date.ToString("yyyy-MM-dd"));

            return Helper.CallApi<object>(url);
        }

        public object ReportRegisterByDate(DateTime startDate, DateTime endDate)
        {
            string url = String.Format(BASE_ADDRESS + "/sttregistpartner?pkey={0}&startdate={1}&enddate={1}",
                PKEY, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            return Helper.CallApi<object>(url);
        }
    }
}
