using Microsoft.Extensions.Configuration;

namespace ProjectManagement.Api.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public DatabaseConfiguration Configuration { get; }

        public DatabaseFactory(IConfiguration configuration)
        {
            var configSection = configuration.GetSection("DataAccess:Merch");
            
            Configuration = new DatabaseConfiguration(
                configSection.GetValue<string>("Server"),
                configSection.GetValue<string>("Username"),
                configSection.GetValue<string>("Password")
                );
        }
    }
}