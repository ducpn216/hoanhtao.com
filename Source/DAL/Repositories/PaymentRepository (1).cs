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
        public bool Insert(string username, string transId, int amount, string refCode, long orderId, int flag)
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

        public long GetOrderId()
        {
           long orderId = -1;

            SqlParameter[] _parameters = {};

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
    }
}
