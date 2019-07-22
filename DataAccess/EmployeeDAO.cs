using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeDAO
    {
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "select * from Employee where Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //employee.Id = Int32.Parse(reader["Id"].ToString());
                        employee.Id = id;
                        employee.LastName = reader["LastName"].ToString();
                        employee.FirstName = reader["FirstName"].ToString();
                        employee.TaskId = Int32.Parse(reader["TaskId"].ToString());
                        employee.UserType = Int32.Parse(reader["UserType"].ToString());
                    }
                }
                connection.Close();
            }
            return employee;
        }
    }
}
