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
    public class InsideRepository1
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
    }
}
