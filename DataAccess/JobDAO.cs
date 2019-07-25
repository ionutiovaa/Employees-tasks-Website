using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class JobDAO
    {
        public void AddTask(Job job)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                DateTime date = DateTime.Now;
                connection.Open();
                string query = "insert into Task(Name, Description, NumberOfHours, EmployeeId, ProjectId, Data) values (@name, @description, @nrHours, @employeeId, @projectId, @data);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", job.Name);
                command.Parameters.AddWithValue("@description", job.Description);
                command.Parameters.AddWithValue("@nrHours", job.NumberOfHours);
                command.Parameters.AddWithValue("@employeeId", job.EmployeeId);
                command.Parameters.AddWithValue("@projectId", job.ProjectId);
                command.Parameters.AddWithValue("data", date.ToString("MM/dd/yyyy hh:mm tt"));
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }
    }
}
