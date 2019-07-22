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

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "select * from Employee where UserType = 2";
                SqlCommand command = new SqlCommand(query, connection);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            TaskId = Int32.Parse(reader["TaskId"].ToString()),
                            UserType = Int32.Parse(reader["UserType"].ToString())
                        };
                        employees.Add(employee);
                    }
                }
                connection.Close();
            }
            return employees;
        }
    }
}
