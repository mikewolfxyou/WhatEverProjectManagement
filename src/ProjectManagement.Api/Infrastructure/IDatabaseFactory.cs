using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Api.Infrastructure
{
    public interface IDatabaseFactory
    {
        Task<MySqlConnection> CreateConnection();
    }
}