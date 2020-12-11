using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProjectManagement.Api.Infrastructure;
using ProjectManagement.Api.Infrastructure.Database;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public class DepartmentDao : IDepartmentDao
    {
        private readonly IDatabaseFactory _databaseFactory;

        public DepartmentDao(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<Department> GetDepartmentAsync(int departmentId)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            return (await connection
                .QueryAsync<Department>(
                    "SELECT * FROM project_management.department WHERE id = @Ids",
                    new {Ids = departmentId}
                )).First();
        }
    }
}