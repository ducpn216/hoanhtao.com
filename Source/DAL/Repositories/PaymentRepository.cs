using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PaymentRepository
    {
        public bool TransferCard(string username, string transId, int amount, string refCode, long orderId, int flag)
        {
            bool isSuccess = false;

            SqlParameter[] _parameters = {
                        new SqlParameter("@TransID", transId),
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Amount", amount),
                        new SqlParameter("@RefCode", refCode),
                        new SqlParameter("@OrderID", orderId),
                        new SqlParameter("@Flag", flag)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "sp_TransferCard", _parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    int outputFlag = Convert.ToInt32(reader["Flag_out"]);

                    if (outputFlag != 999 && outputFlag != 9999)
                    {
                        isSuccess = true;
                    }
                }
            }

            return isSuccess;
        }

        public bool TransferCardTest(string transId, int userId, int amount, int gold, string refCode,
            string pin, string serial, int flag, string ip, string description)
        {
            bool isSuccess = false;

            SqlParameter[] _parameters = {
                        new SqlParameter("@TransID", transId),
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@Amount", amount),
                        new SqlParameter("@Gold", gold),
                        new SqlParameter("@RefCode", refCode),
                        new SqlParameter("@Flag", flag),
                        new SqlParameter("@Pin", pin),
                        new SqlParameter("@Serial", serial),
                        new SqlParameter("@IP", ip),
                        new SqlParameter("@Description", description)              
                                         };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "sp_TransferCard_1", _parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    int outputFlag = Convert.ToInt32(reader["Flag_out"]);

                    if (outputFlag != 999 && outputFlag != 9999)
                    {
                        isSuccess = true;
                    }
                }
            }

            return isSuccess;
        }

        public bool TransferGoldToGame(int userId, int serverId, long orderId, int knb, int flag, string ip)
        {
            bool isSuccess = false;

            SqlParameter[] _parameters = {
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@ServerID", serverId),
                        new SqlParameter("@OrderID", orderId),
                        new SqlParameter("@Gold", knb),
                        new SqlParameter("@Flag", flag),
                        new SqlParameter("@IP", ip)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "sp_TransferToGame", _parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    int outputFlag = Convert.ToInt32(reader["flag"]);

                    if (outputFlag == 0)
                    {
                        isSuccess = true;
                    }
                }
            }

            return isSuccess;
        }

        public long GetOrderId()
        {
            long orderId = -1;

            SqlParameter[] _parameters = { };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsGetOderId", _parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    orderId = Convert.ToInt64(reader["OrderID"]);
                }
            }

            return orderId;
        }

        public long GetOrderIdTest()
        {
            long orderId = -1;

            SqlParameter[] _parameters = { };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsGetOderId_1", _parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    orderId = Convert.ToInt64(reader["OrderID"]);
                }
            }

            return orderId;
        }
    }
}
