using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Newbie_MVC.Models
{
    public class RoleModel
    {
        public int role_id { get; set; }
        public string role { get; set; }
    }

    public class RoleDb
    {
        public static SqlConnection InitialConnection()
        {
            SqlDataSource sqlDS = new SqlDataSource();
            sqlDS.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NewbieMVCDBService"].ConnectionString;

            return new SqlConnection(sqlDS.ConnectionString);
        }

        public static List<RoleModel> selectRole()
        {
            List<RoleModel> roleList = new List<RoleModel>();
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();

                var sqlStr = @"Select * From SYSTEMROLE";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    RoleModel model = new RoleModel();
                    {
                        model.role_id = int.Parse(reader["ROLE_ID"].ToString());
                        model.role = reader["ROLE_NAME"].ToString();
                    }
                    roleList.Add(model);
                }
            }

            return roleList;
        }
    }
}