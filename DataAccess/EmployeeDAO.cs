using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
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

        public dynamic GetEmployeeByName(string lastName, string firstName)
        {
            Employee employee = new Employee();
            Job task = new Job();
            Project project = new Project();
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query1 = "select * from Employee where LastName LIKE @lastName and FirstName LIKE @FirstName";
                SqlCommand command1 = new SqlCommand(query1, connection);
                command1.Parameters.AddWithValue("@lastName", lastName);
                command1.Parameters.AddWithValue("@firstName", firstName);
                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee.Id = Int32.Parse(reader["Id"].ToString());
                        employee.FirstName = firstName;
                        employee.LastName = lastName;
                        employee.TaskId = Int32.Parse(reader["TaskId"].ToString());
                        employee.UserType = Int32.Parse(reader["UserType"].ToString());
                    }
                }
                string query2 = "select * from Task where Id = @id";
                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@id", employee.TaskId);
                using (SqlDataReader reader1 = command2.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        task.Id = employee.TaskId;
                        task.Name = reader1["Name"].ToString();
                        task.NumberOfHours = float.Parse(reader1["NumberOfHours"].ToString());
                        task.ProjectId = Int32.Parse(reader1["ProjectId"].ToString());
                    }
                }
                string query3 = "select * from Project where Id = @idp";
                SqlCommand command3 = new SqlCommand(query3, connection);
                command3.Parameters.AddWithValue("@idp", task.ProjectId);
                using (SqlDataReader reader2 = command3.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        project.Id = task.ProjectId;
                        project.Name = reader2["Name"].ToString();
                    }
                }
                connection.Close();
            }
            dynamic toReturn = new ExpandoObject();
            toReturn.Description = task.Name;
            toReturn.NumberOfHours = task.NumberOfHours;
            toReturn.ProjectName = project.Name;
            return toReturn;
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
