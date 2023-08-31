using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CRUD_Example
{
    public static class Connection
    {
        
        public static IConfiguration _config;
        public static SqlConnection GetSqlConnection()
        {      
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _config.GetConnectionString("AttendanceAppCon");
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            return sqlConnection;
        }
    }
}
