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
    public class InsideRepository
    {
        public ResultResponse ChangePassword(string username, string oldPass, string newPass)
        {
            ResultResponse response = new ResultResponse();

            SqlParameter[] _parameters = {
                        new SqlParameter("@Username", username),
                        new SqlParameter("@PassOld", oldPass),
                        new SqlParameter("@PassNew", newPass)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.InsideConnectionString, "sp_ChangePassword", _parameters))
            {
                if (reader.HasRows && reader.Read())
                {
                    response.Flag = Convert.ToInt32(reader["Flag"]);
                    response.Message = reader["Description"].ToString();
                }
            }

            return response;
        }

        public int LoginInside(string username, string password, string IP, ref int userId, ref int groupId)
        {
            int flag = -1;

            SqlParameter[] _parameters = {
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@IP", IP)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.InsideConnectionString, "udsUsers_Login", _parameters))
            {
                if (reader.HasRows && reader.Read())
                {
                    flag = Convert.ToInt32(reader["Flag"]);

                    if (reader["UserId"] != null && !(reader["UserId"] is DBNull))
                    {
                        userId = Convert.ToInt32(reader["UserId"]);
                        groupId = Convert.ToInt32(reader["GroupId"]);
                    }
                }
            }

            return flag;
        }

        public int CacheCCU(int ccu, int serverId, DateTime date)
        {
            int flag = -1;
            SqlParameter[] _parameters = {
                        new SqlParameter("@CCU", ccu),
                        new SqlParameter("@server_id", serverId),
                        new SqlParameter("@date", date)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.InsideConnectionString, "sp_CacheCCU", _parameters))
            {
                if (reader.HasRows && reader.Read())
                {
                    flag = Convert.ToInt32(reader["Flag"]);
                }
            }

            return flag;

        }

        public int GetNRU(DateTime from, DateTime to)
        {
            int num = 0;

            SqlParameter[] _parameters = {
                        new SqlParameter("@fromdate", from),
                        new SqlParameter("@todate", to)
                                             };

            //DataSet dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.InsideConnectionString, "sp_GetNRU", _parameters);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.InsideConnectionString, "sp_GetNRU", _parameters))
            {
                if (reader.HasRows && reader.Read())
                {
                    num = Convert.ToInt32(reader["NRU"]);
                }
            }

            return num;
        }

        public DataSet GetCCU(int serverID, DateTime date)
        {
            DataSet dataSet = new DataSet();

            SqlParameter[] _parameters = {
                        new SqlParameter("@Date", date),
                        new SqlParameter("@ServerID", serverID)
                                             };

            dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.InsideConnectionString, "udsGetCCU", _parameters);

            return dataSet;
        }

        public DataSet GetReport(int serverID, DateTime from, DateTime to)
        {
            DataSet dataSet = new DataSet();

            SqlParameter[] _parameters = {
                        new SqlParameter("@DateF", from),
                        new SqlParameter("@DateT", to),
                        new SqlParameter("@ServerID", serverID)
                                             };

            dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.InsideConnectionString, "udsReportInside", _parameters);

            return dataSet;
        }

        public DataSet ReportMarketing(string pid, DateTime from, DateTime to)
        {
            DataSet dataSet = new DataSet();

            SqlParameter[] _parameters = {
                        new SqlParameter("@pid", pid),
                        new SqlParameter("@DateF", from),
                        new SqlParameter("@DateT", to)
                                             };

            dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.InsideConnectionString, "sp_getRptMkt", _parameters);

            return dataSet;
        }

        public DataSet GetCREV(DateTime date)
        {
            DataSet dataSet = new DataSet();

            SqlParameter[] _parameters = {
                        new SqlParameter("@Date", date)
                                             };

            dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.InsideConnectionString, "RptREV_ByDate", _parameters);

            return dataSet;
        }

        public int GetCREVTotal()
        {
            int total = 0;

            SqlParameter[] _parameters = {};

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.InsideConnectionString, "RptREV", _parameters))
            {
                if (reader.HasRows && reader.Read())
                {
                    total = Convert.ToInt32(reader["Total"]);
                }
            }

            return total;
            
        }

        public DataSet GetTransferHistory(string username)
        {
            DataSet dataSet = new DataSet();

            SqlParameter[] _parameters = {
                        new SqlParameter("@Username", username)
                                             };

            dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.InsideConnectionString, "RptTransferHistory", _parameters);

            return dataSet;
        }
    }
}
