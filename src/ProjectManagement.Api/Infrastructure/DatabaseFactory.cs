using Microsoft.Extensions.Configuration;

namespace ProjectManagement.Api.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public DatabaseConfiguration Configuration { get; }

        public DatabaseFactory(IConfiguration configuration)
        {
            var configSection = configuration.GetSection("DataAccess:Merch");
            
            Configuration = new DatabaseConfiguration {
                Server = configSection.GetValue<string>("Server"),
                Username = configSection.GetValue<string>("Username"),
                Password = configSection.GetValue<string>("Password")
            };
        }
    }
}