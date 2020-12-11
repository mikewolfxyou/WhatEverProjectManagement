using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProjectManagement.Api.Infrastructure;
using ProjectManagement.Api.Infrastructure.Database;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public class EmployeeRoleDao : IEmployeeRoleDao
    {
        private readonly IDatabaseFactory _databaseFactory;

        public EmployeeRoleDao(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<EmployeeRole> GetEmployeeRole(int employeeRoleId)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            return (await connection
                .QueryAsync<EmployeeRole>(
                    "SELECT * FROM project_management.employeeRole WHERE id = @Ids",
                    new {Ids = employeeRoleId}
                )).First();
        }
    }
}