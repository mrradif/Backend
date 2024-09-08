using DAL.Dapper_Object;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace DAL.Context.Dapper
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection(string dbName)
        {
            if (string.IsNullOrEmpty(dbName) || string.IsNullOrWhiteSpace(dbName))
                throw new NullReferenceException("Database not found");
            _connectionString = Database.GetConnectionString(dbName);
            return new SqlConnection(_connectionString);
        }


        public IDbConnection CreateConnection(long clientId)
        {
            if (clientId == 0)
                throw new NullReferenceException("Database not found");
            _connectionString = string.Empty;
            return new SqlConnection(_connectionString);
        }

        public IDbConnection EstablishRemoteConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
                throw new NullReferenceException("Database not found");
            _connectionString = connectionString;
            return new SqlConnection(_connectionString);
        }
    }
}
