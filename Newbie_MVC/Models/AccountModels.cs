using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Newbie_MVC.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage="請輸入帳號")]
        [Display( Name = "使用者名稱" )]
        public string UserName { get; set; }

        [Required(ErrorMessage="請輸入密碼")]
        [DataType( DataType.Password )]
        [Display( Name = "密碼" )]
        public string Password { get; set; }

        [Display( Name = "記住我?" )]
        public bool RememberMe { get; set; }

        public int Role { get; set; }

    }
    /*
    public class AccountDbContext : DbContext
    {

    }
    */

    public class AccountDb
    {
        public List<LogOnModel> DbConnection(string user, string pass)
        {
            SqlDataSource sqlDS = new SqlDataSource();
            sqlDS.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NewbieMVCDBService"].ConnectionString; 
            
            List<LogOnModel> userList = new List<LogOnModel>();
            using (SqlConnection conn = new SqlConnection(sqlDS.ConnectionString))
            {
                conn.Open();

                var sqlStr = @"Select USER_ACCOUNT, USER_PASSWORD, ROLE_ID From ACCOUNT Where USER_ACCOUNT='" + user + "' And USER_PASSWORD='" + pass + "'";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    LogOnModel model = new LogOnModel();
                    {
                        model.UserName = reader["USER_ACCOUNT"].ToString();
                        model.Password = reader["USER_PASSWORD"].ToString();
                        model.Role = int.Parse(reader["ROLE_ID"].ToString());
                    }
                    userList.Add(model);
                }
                conn.Close();
            }
            return userList;
        }
    }
}