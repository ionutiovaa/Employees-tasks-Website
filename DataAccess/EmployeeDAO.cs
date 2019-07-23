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
                        employee.UserType = Int32.Parse(reader["UserType"].ToString());
                    }
                }
                connection.Close();
            }
            return employee;
        }

        public List<dynamic> GetEmployeeByName(string lastName, string firstName)
        {
            List<Job> jobs = new List<Job>();
            List<Project> projects = new List<Project>();
            List<dynamic> tasks = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                Employee employee = new Employee();
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
                        employee.UserType = Int32.Parse(reader["UserType"].ToString());
                    }
                }
                string query2 = "select * from Task where EmployeeId = @id";
                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@id", employee.Id);
                using (SqlDataReader reader1 = command2.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        Job job = new Job
                        {
                            Id = Int32.Parse(reader1["Id"].ToString()),
                            Name = reader1["Name"].ToString(),
                            Description = reader1["Description"].ToString(),
                            NumberOfHours = float.Parse(reader1["NumberOfHours"].ToString()),
                            EmployeeId = employee.Id,
                            ProjectId = Int32.Parse(reader1["ProjectId"].ToString())
                        };
                        jobs.Add(job);
                    }
                }
                foreach(var j in jobs)
                {
                    string query3 = "select * from Project where Id = @idp";
                    SqlCommand command3 = new SqlCommand(query3, connection);
                    command3.Parameters.AddWithValue("@idp", j.ProjectId);
                    using (SqlDataReader reader2 = command3.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            Project project = new Project
                            {
                                Id = j.ProjectId,
                                Name = reader2["Name"].ToString()
                            };
                            projects.Add(project);
                            dynamic task = new ExpandoObject();
                            task.LastName = employee.LastName;
                            task.FirstName = employee.FirstName;
                            task.TaskName = j.Name;
                            task.Description = j.Description;
                            task.NumberOfHours = j.NumberOfHours;
                            task.ProjectName = reader2["Name"].ToString();
                            tasks.Add(task);
                        }
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public void AddEmployee(dynamic employee)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "insert into Employee(LastName, FirstName, Username, Password, UserType) values (@lastName, @firstName, @username, @password, @userType);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@username", employee.Username);
                command.Parameters.AddWithValue("@password", employee.Password);
                command.Parameters.AddWithValue("@userType", employee.UserType);
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }

        public void DeleteEmployee(string lastName, string firstName)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "delete from Employee where LastName LIKE @lastName and FirstName LIKE @firstName;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@firstName", firstName);
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }

        public void EditEmployee(int id, string lastName, string firstName, string username, string password, int userType)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "update Employee set LastName = @lastname, FirstName = @firstName, Username = @username, Password = @password, UserType = @userType where Id = @id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@userType", userType);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int GetIdEmployee(string username, string password)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "select Id from Employee where Username LIKE @username and Password LIKE @password;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = Int32.Parse(reader["Id"].ToString());
                }
                connection.Close();
            }
            return id;
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
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
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
