using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Newbie_MVC.Models
{
    public class EmployeeModel
    {
        public int id { set; get; }
        public string name { set; get; }
        public string title { set; get; }
        public string birth { set; get; }
        public string hire { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public string extension { set; get; }
        public string photopath { set; get; }
        public string notes { set; get; }
        public int managerId { set; get; }
        public int salary { set; get; }
    }

    public class EmployeeDb
    {
        public SqlConnection InitialConnection()
        {
            SqlDataSource sqlDS = new SqlDataSource();
            sqlDS.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NewbieMVCDBService"].ConnectionString;

            return new SqlConnection(sqlDS.ConnectionString);
        }

        public List<EmployeeModel> select()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();
                var sqlStr = @"Select * From EMPLOYEE";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeModel model = new EmployeeModel();
                    {
                        model.id = int.Parse(reader["EMPLOYEE_ID"].ToString());
                        model.name = reader["EMPLOYEE_Name"].ToString();
                        model.title = reader["EMPLOYEE_Title"].ToString();
                        model.birth = reader["EMPLOYEE_BirthDate"].ToString();
                        model.hire = reader["EMPLOYEE_HireDate"].ToString();
                        model.address = reader["EMPLOYEE_Address"].ToString();
                        model.phone = reader["EMPLOYEE_HomePhone"].ToString();
                        model.extension = reader["EMPLOYEE_Extension"].ToString();
                        model.photopath = reader["EMPLOYEE_PhotoPath"].ToString();
                        model.notes = reader["EMPLOYEE_Notes"].ToString();
                        model.managerId = int.Parse(reader["EMPLOYEE_ManagerID"].ToString());
                        model.salary = int.Parse(reader["EMPLOYEE_Salary"].ToString());
                    }
                    employeeList.Add(model);
                }
            }

            return employeeList;
        }

        public void insert(EmployeeModel m)
        {
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();
                var sqlStr = @"Insert Into EMPLOYEE(EMPLOYEE_Name, EMPLOYEE_Title, EMPLOYEE_BirthDate, EMPLOYEE_HireDate, EMPLOYEE_Address, EMPLOYEE_HomePhone,"+
                    " EMPLOYEE_Extension, EMPLOYEE_PhotoPath, EMPLOYEE_Notes, EMPLOYEE_ManagerId, EMPLOYEE_Salary)" + 
                    " Values (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11) ";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                sqlCmd.Parameters.AddWithValue("@param1", m.name);
                sqlCmd.Parameters.AddWithValue("@param2", m.title);
                sqlCmd.Parameters.AddWithValue("@param3", m.birth);
                sqlCmd.Parameters.AddWithValue("@param4", m.hire);
                sqlCmd.Parameters.AddWithValue("@param5", m.address);
                sqlCmd.Parameters.AddWithValue("@param6", m.phone);
                sqlCmd.Parameters.AddWithValue("@param7", m.extension);
                sqlCmd.Parameters.AddWithValue("@param8", m.photopath);
                sqlCmd.Parameters.AddWithValue("@param9", m.notes);
                sqlCmd.Parameters.AddWithValue("@param10", m.managerId);
                sqlCmd.Parameters.AddWithValue("@param11", m.salary);
                sqlCmd.ExecuteNonQuery();
            }
        }

        public EmployeeModel find(int id)
        {
            EmployeeModel model = new EmployeeModel();
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();
                var sqlStr = @"Select * From EMPLOYEE Where EMPLOYEE_ID=@param1";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);
                sqlCmd.Parameters.AddWithValue("@param1", id);

                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    model.id = int.Parse(reader["EMPLOYEE_ID"].ToString());
                    model.name = reader["EMPLOYEE_Name"].ToString();
                    model.title = reader["EMPLOYEE_Title"].ToString();
                    model.birth = reader["EMPLOYEE_BirthDate"].ToString();
                    model.hire = reader["EMPLOYEE_HireDate"].ToString();
                    model.address = reader["EMPLOYEE_Address"].ToString();
                    model.phone = reader["EMPLOYEE_HomePhone"].ToString();
                    model.extension = reader["EMPLOYEE_Extension"].ToString();
                    model.photopath = reader["EMPLOYEE_PhotoPath"].ToString();
                    model.notes = reader["EMPLOYEE_Notes"].ToString();
                    model.managerId = int.Parse(reader["EMPLOYEE_ManagerID"].ToString());
                    model.salary = int.Parse(reader["EMPLOYEE_Salary"].ToString());
                }
                    conn.Close();
            }
            return model;
        }

        public void edit(int id, EmployeeModel m)
        {
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();
                var sqlStr = @"Update EMPLOYEE Set EMPLOYEE_Name=@param1, EMPLOYEE_Title=@param2, EMPLOYEE_BirthDate=@param3, EMPLOYEE_HireDate=@param4, EMPLOYEE_Address=@param5," +
                    " EMPLOYEE_HomePhone=@param6, EMPLOYEE_Extension=@param7, EMPLOYEE_PhotoPath=@param8, EMPLOYEE_Notes=@param9," +
                    " EMPLOYEE_ManagerId=@param10, EMPLOYEE_Salary=@param11" + 
                    " Where EMPLOYEE_ID=@param12";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                sqlCmd.Parameters.AddWithValue("@param1", m.name);
                sqlCmd.Parameters.AddWithValue("@param2", m.title);
                sqlCmd.Parameters.AddWithValue("@param3", m.birth);
                sqlCmd.Parameters.AddWithValue("@param4", m.hire);
                sqlCmd.Parameters.AddWithValue("@param5", m.address);
                sqlCmd.Parameters.AddWithValue("@param6", m.phone);
                sqlCmd.Parameters.AddWithValue("@param7", m.extension);
                sqlCmd.Parameters.AddWithValue("@param8", m.photopath);
                sqlCmd.Parameters.AddWithValue("@param9", m.notes);
                sqlCmd.Parameters.AddWithValue("@param10", m.managerId);
                sqlCmd.Parameters.AddWithValue("@param11", m.salary);
                sqlCmd.Parameters.AddWithValue("@param12", id);
                sqlCmd.ExecuteNonQuery();
            }
        }

        public void delete(int id)
        {
            using (SqlConnection conn = InitialConnection())
            {
                conn.Open();
                var sqlStr = @"Delete From Employee Where EMPLOYEE_ID=@param1";
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);

                sqlCmd.Parameters.AddWithValue("@param1", id);
                sqlCmd.ExecuteNonQuery();
            }
        }
    }
}