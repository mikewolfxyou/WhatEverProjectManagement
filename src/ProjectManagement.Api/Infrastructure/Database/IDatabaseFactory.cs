using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Api.Infrastructure.Database
{
    public interface IDatabaseFactory
    {
        Task<MySqlConnection> CreateConnection();
    }
}