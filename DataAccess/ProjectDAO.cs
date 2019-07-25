using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProjectDAO
    {
        public int GetProjectIdByName(string name)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                string query = "select Id from Project where Name LIKE @name;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = Int32.Parse(reader["Id"].ToString());
                }
                connection.Close();
            }
            return id;
        }
    }
}
