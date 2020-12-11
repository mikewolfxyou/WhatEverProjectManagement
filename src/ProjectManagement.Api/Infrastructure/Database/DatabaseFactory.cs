using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Api.Infrastructure.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private DatabaseConfiguration Configuration { get; }

        public DatabaseFactory(IConfiguration configuration)
        {
            var configSection = configuration.GetSection("DataAccess:Merch");

            Configuration = new DatabaseConfiguration
            {
                Server = configSection.GetValue<string>("Server"),
                Username = configSection.GetValue<string>("Username"),
                Password = configSection.GetValue<string>("Password"),
                Database = configSection.GetValue<string>("Database")
            };
        }

        public async Task<MySqlConnection> CreateConnection()
        {
            var dbConnection = new MySqlConnection(
                $"server={Configuration.Server};userid={Configuration.Username};" +
                $"password={Configuration.Password};database={Configuration.Database}");

            await dbConnection.OpenAsync();

            return dbConnection;
        }
    }
}