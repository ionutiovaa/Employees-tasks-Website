using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
    public static class DbConnection
    {
        static readonly public string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
    }
}
