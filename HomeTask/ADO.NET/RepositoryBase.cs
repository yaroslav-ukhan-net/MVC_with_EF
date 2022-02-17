using System;
using System.Data.SqlClient;

namespace ADO.NET
{
    public class RepositoryBase
    {
        private string _connectionString = @"Data Source=DESKTOP-G7US54D\SQLEXPRESS;Initial Catalog=University;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}