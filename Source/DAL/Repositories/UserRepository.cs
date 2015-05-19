using Domain.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Domain;
using Core;
using DAL.Reponses;

namespace DAL.Repositories
{
    public class UserRepository
    {
        //public static Enums.UserGroup GetUserGroup(int groupId)
        //{
        //    var values = Enum.GetValues(typeof(Enums.UserGroup)).Cast<Enums.UserGroup>();

        //    foreach (Enums.UserGroup item in values)
        //    {
        //        if (groupId == (int)item)
        //        {
        //            return item;
        //        }
        //    }

        //    return Enums.UserGroup.Guest;
        //}

        //string className = "Domain.Business.User";

        public int Register(string username, string password1, string password2, string email, int questionId, string answer,
            string phoneNumber, string identityCard, string snoNumber, string ip, string pid, ref int flag, ref string message)
        {
            int userId = -1;

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", username),
                        new SqlParameter("@user_pwd", password1),
                        new SqlParameter("@user_pwd2", password2),
                        new SqlParameter("@user_mail", email),
                        new SqlParameter("@user_question", questionId),
                        new SqlParameter("@user_answer", answer),
                        new SqlParameter("@IdentityCard", identityCard),
                        new SqlParameter("@PhoneNumber", phoneNumber),
                        //new SqlParameter("@sno__numb", snoNumber),
                        new SqlParameter("@ip", ip),
                        new SqlParameter("@pid", pid)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUsers_Insert", _parameters))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        if (reader["UserID"] != null && !(reader["UserID"] is DBNull))
                        {
                            userId = Convert.ToInt32(reader["UserID"]);
                        }

                        flag = Convert.ToInt32(reader["Flag"]);
                        message = reader["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "Register", ex.ToString());
            }
            
            return userId;
        }

        public int GetAccountId(string accountName)
        {
            int accountId = -1;

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@account_name", accountName)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "get_account_id_by_account_name", _parameters))
                {
                    if (reader.Read())
                    {
                        if (reader["account_id"] != null && !(reader["account_id"] is DBNull))
                        {
                            accountId = Convert.ToInt32(reader["account_id"]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetAccountId", ex.ToString());
            }
           
            return accountId;
        }

        public string GetAccountName(int accountId)
        {
            string accountName = "";

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@account_id", accountId)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "get_account_name_by_account_id", _parameters))
                {
                    if (reader.Read())
                    {
                        accountName = reader["account_name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetAccountName", ex.ToString());
            }
            
            return accountName;
        }

        public MessageView ChangePassword(string username, string newPass, string pass2)
        {
            MessageView view = new MessageView();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", username),
                        new SqlParameter("@user_pwd2",pass2),
                        new SqlParameter("@user_pwdnew", newPass)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUser_ChangePwd1", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        view.Flag = Convert.ToInt32(reader["Flag"]);
                        view.Message = reader["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "ChangePassword", ex.ToString());
            }
            

            return view;
        }

        public ResultResponse ChangePassword2(string username, string pass2, string newPass)
        {
            ResultResponse view = new ResultResponse();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", username),
                        new SqlParameter("@user_pwd2",pass2),
                        new SqlParameter("@user_pwdnew", newPass)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUser_ChangePwd2", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        view.Flag = Convert.ToInt32(reader["Flag"]);
                        view.Message = reader["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "ChangePassword2", ex.ToString());
            }
            

            return view;
        }

        public ResultResponse ForgetPassword(string username, string email)
        {
            ResultResponse view = new ResultResponse();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", username),
                        new SqlParameter("@user_email", email)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUser_ForgetPwd2", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        view.Flag = Convert.ToInt32(reader["Flag"]);
                        view.Message = reader["Description"].ToString();

                        if (view.Flag == 0)
                        {
                            view.Message = reader["NewPassword"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "ForgetPassword", ex.ToString());
            }
            
            return view;
        }

        public MessageView ForgetPassword2(string username, string email, int questionId, string answer, ref string newPassword)
        {
            MessageView view = new MessageView();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", username),
                        new SqlParameter("@user_email", email),
                        new SqlParameter("@user_question", questionId),
                        new SqlParameter("@user_answer", answer)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUser_ForgetPwd2", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        view.Flag = Convert.ToInt32(reader["Flag"]);
                        view.Message = reader["Description"].ToString();

                        if (view.Flag == 0)
                        {
                            newPassword = reader["NewPassword"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "ForgetPassword2", ex.ToString());
            }
            
            return view;
        }

        public int Login(string username, string password, ref int flag)
        {
            int userId = -1;

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Password", password)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUsers_Login", _parameters))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        if (reader["UserId"] != null && !(reader["UserId"] is DBNull))
                        {
                            userId = Convert.ToInt32(reader["UserId"]);
                            flag = Convert.ToInt32(reader["Flag"]);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "Login", ex.ToString());
            }
            
            return userId;
        }

        public int LoginSocial(string username, Enums.LoginType loginType, ref int flag)
        {
            int userId = -1;

            if (loginType == Enums.LoginType.Facebook)
            {
                username = username + "@fb.com";
            }
            else if (loginType == Enums.LoginType.Google)
            {
                username = username + "@gg.com";
            }
            SqlParameter[] _parameters = {
                        new SqlParameter("@Username", username)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "login_fb", _parameters))
            {
                if (reader.HasRows && reader.Read())
                {
                    if (reader["UserID"] != null && !(reader["UserID"] is DBNull))
                    {
                        userId = Convert.ToInt32(reader["UserID"]);
                        flag = Convert.ToInt32(reader["Flag"]);
                    }

                }
            }

            return userId;
        }

        public int LoginInside(string username, string password, string IP, ref int userId, ref int groupId)
        {
            int flag = -1;

            SqlParameter[] _parameters = {
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@IP", IP)
                                             };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUsers_Login", _parameters))
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

        public DataSet GetUserInfo(int userId)
        {
            DataSet dataSet = new DataSet();

            try
            {
                SqlParameter[] _parameters = {
                    new SqlParameter("@UserId", userId)
                                             };

                dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "udsGetUserInfo", _parameters);
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetUserInfo", ex.ToString());
            }
           
            return dataSet;
        }

        public DataSet GetLevelInfo(string account)
        {
            DataSet dataSet = new DataSet();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@CharName", account)
                                             };

                dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "sp_TraCuuThongTinLevel", _parameters);

            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetUserInfo", ex.ToString());
            }
            
            return dataSet;
        }

        public DataSet GetLevelInfo_TanThu(string account)
        {
            DataSet dataSet = new DataSet();

            SqlParameter[] _parameters = {
                        new SqlParameter("@CharName", account)
                                             };

            dataSet = SqlHelper.ExecuteDataset(ConnectionConfig.WebConnectionString, "sp_TraCuuThongTinLevel_TanThu", _parameters);

            return dataSet;
        }
        //public DataSet GetList()
        //{
        //    DataSet dataSet = new DataSet();

        //    try
        //    {
        //        SqlParameter[] _parameters = {
        //                                     };

        //        SqlHelper.ExecuteNonQuery(Config.SqlConnectionString, CommandType.Text, "select * from Users", _parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorProvider.WriteErrorToLogFile(Config.ErrorFilePath, "", className, "GetList()", ex.ToString());
        //    }

        //    return dataSet;
        //}

        public MessageView UpdatePlayedServer(int userId, int serverId)
        {
            MessageView view = new MessageView();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", userId),
                        new SqlParameter("@server_id",serverId)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "update_played_server", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        view.Flag = Convert.ToInt32(reader["Flag"]);
                        view.Message = reader["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "UpdatePlayedServer", ex.ToString());
            }
            

            return view;
        }

        public int GetRecentServer(int userId)
        {
            int serverId = -1;

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@user_id", userId)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "get_recent_server", _parameters))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        if (reader["server_id"] != null && !(reader["server_id"] is DBNull))
                        {
                            serverId = Convert.ToInt32(reader["server_id"]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetRecentServer", ex.ToString());
            }
            

            return serverId;
        }

        public ResultResponse UpdateProfile(int userId, string email, string cmnd, string phone)
        {
            ResultResponse view = new ResultResponse();

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@Userid", userId),
                        new SqlParameter("@Email", email),
                        new SqlParameter("@CMND", cmnd),
                        new SqlParameter("@Phone", phone)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "udsUpdateInfo", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        view.Flag = Convert.ToInt32(reader["Flag"]);
                        view.Message = reader["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "UpdateProfile", ex.ToString());
            }
            

            return view;
        }

        public int GetGold(int userId)
        {
            int gold = 0;

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@UserID", userId)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.WebConnectionString, "sp_GetGold", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        gold = Convert.ToInt32(reader["Gold"]);
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetGold", ex.ToString());
            }


            return gold;
        }

        public int GetKnbInGame(int userId)
        {
            int gold = 0;

            try
            {
                SqlParameter[] _parameters = {
                        new SqlParameter("@UserID", userId)
                                             };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionConfig.InsideConnectionString, "getKNB", _parameters))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        gold = Convert.ToInt32(reader["KNB"]);
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Repository", "GetKnbInGame", ex.ToString());
            }


            return gold;
        }
    }
}
