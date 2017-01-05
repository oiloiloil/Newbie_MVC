using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Newbie_MVC.Models
{
    public class UserAccountModel
    {
        public string UserName { set; get; } // 帳號
        public string Name { set; get; } // 姓名
        public string Password { set; get; }
        public string CreateDate { set; get; }
        public string LatestLoginDate { set; get; }
        public string Role { set; get; }
        public string Enable { set; get; } // 帳號啟用
    }
    
    public class UserAccountDb
    {
        List<RoleModel> roleList = new List<RoleModel>();

        public UserAccountDb()
        {
            roleList = RoleDb.selectRole(); // 找出所有的role
        }

        public SqlConnection InitialConnection()
        {
            SqlDataSource sqlDS = new SqlDataSource();
            sqlDS.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NewbieMVCDBService"].ConnectionString;

            return new SqlConnection(sqlDS.ConnectionString);
        }

        public List<UserAccountModel> SelectWithUser(string user)
        {
            List<UserAccountModel> userList = new List<UserAccountModel>();
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();

                var sqlStr = @"Select * From ACCOUNT Where USER_ACCOUNT='" + user + "'";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    UserAccountModel model = new UserAccountModel();
                    {
                        model.UserName = reader["USER_ACCOUNT"].ToString();
                        model.Password = reader["USER_PASSWORD"].ToString();
                        model.Name = reader["USER_NAME"].ToString();
                        model.CreateDate = reader["CREATE_DATE"].ToString();
                        model.LatestLoginDate = reader["LAST_LOGIN_DATE"].ToString();

                        setRole(model, int.Parse(reader["ROLE_ID"].ToString()));
                        int enable = reader.GetOrdinal("USER_ENABLE");
                        setEnable(model, (bool)reader.GetSqlBoolean(enable));
                    }
                    userList.Add(model);
                }
                conn.Close();
            }
            return userList;
        }

        public void ChangeLatestLoginTime(string user, DateTime time)
        {
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();
                var sqlStr = @"Update ACCOUNT Set LAST_LOGIN_DATE=@param1 Where USER_ACCOUNT=@param2";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);
                sqlCmd.Parameters.AddWithValue("@param1", time.ToString("yyyy-MM-dd HH:mm:ss"));
                sqlCmd.Parameters.AddWithValue("@param2", user);
                sqlCmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void setRole(UserAccountModel model, int role)
        {
            foreach (RoleModel r in roleList)
            {
                if (role == r.role_id)
                {
                    model.Role = r.role;
                    break;
                }
            }
        }

        private void setEnable(UserAccountModel model, bool enable)
        {
            bool ENABLE = true, DISABLE = false;
            if (enable == ENABLE)
                model.Enable = "啟用";
            else if (enable == DISABLE)
                model.Enable = "停用";
        }
    }
}