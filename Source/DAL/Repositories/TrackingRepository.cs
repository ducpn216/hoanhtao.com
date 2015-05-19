using DAL.Reponses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TrackingRepository
    {
        public List<TrackingResponse> GetTurnOverByDate(string pid, DateTime startDate, DateTime endDate)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@start_date", startDate),
                        new SqlParameter("@end_date", endDate)
                                             };

            using (DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsReportTurnOverByDate", _parameters))
            {
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    list = dataSet.Tables[0].AsEnumerable().Select(x => new TrackingResponse()
                    {
                        date = x.Field<DateTime>("date"),
                        totalmoney = x.Field<int>("totalmoney")
                    }).ToList();
                }
            }

            return list;
        }

        public List<TrackingResponse> GetTurnOverByMonth(string pid, int year, int month)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@year", year),
                        new SqlParameter("@month", month)
                                             };

            using (DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsReportTurnOverByMonth", _parameters))
            {
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    list = dataSet.Tables[0].AsEnumerable().Select(x => new TrackingResponse()
                    {
                        date = x.Field<DateTime>("date"),
                        totalmoney = x.Field<int>("totalmoney")
                    }).ToList();
                }
            }

            return list;
        }

        public List<TrackingHourResponse> GetTurnOverByHour(string pid, DateTime date)
        {
            List<TrackingHourResponse> list = new List<TrackingHourResponse>();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@date", date)
                                             };

            using (DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsReportTurnOverByHour", _parameters))
            {
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    list = dataSet.Tables[0].AsEnumerable().Select(x => new TrackingHourResponse()
                    {
                        date = x.Field<int>("date"),
                        totalmoney = x.Field<int>("totalmoney")
                    }).ToList();
                }
            }

            return list;
        }

        public List<TrackingResponse> GetRegisterByDate(string pid, DateTime startDate, DateTime endDate)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@start_date", startDate),
                        new SqlParameter("@end_date", endDate)
                                             };

            using (DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsReportRegisterByDate", _parameters))
            {
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    list = dataSet.Tables[0].AsEnumerable().Select(x => new TrackingResponse()
                    {
                        date = x.Field<DateTime>("date"),
                        totalmoney = x.Field<int>("totalmoney")
                    }).ToList();
                }
            }

            return list;
        }

        public List<TrackingHourResponse> GetRegisterByHour(string pid, DateTime date)
        {
            List<TrackingHourResponse> list = new List<TrackingHourResponse>();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@date", date)
                                             };

            using (DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsReportRegisterByHour", _parameters))
            {
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    list = dataSet.Tables[0].AsEnumerable().Select(x => new TrackingHourResponse()
                    {
                        date = x.Field<int>("date"),
                        totalmoney = x.Field<int>("totalmoney")
                    }).ToList();
                }
            }

            return list;
        }

        public List<TrackingResponse> GetRegisterByMonth(string pid, int year, int month)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@year", year),
                        new SqlParameter("@month", month)
                                             };

            using (DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsReportRegisterByMonth", _parameters))
            {
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    list = dataSet.Tables[0].AsEnumerable().Select(x => new TrackingResponse()
                    {
                        date = x.Field<DateTime>("date"),
                        totalmoney = x.Field<int>("totalmoney")
                    }).ToList();
                }
            }

            return list;
        }
    }
}
