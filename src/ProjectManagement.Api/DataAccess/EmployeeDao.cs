using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ProjectManagement.Api.Infrastructure;

namespace ProjectManagement.Api.DataAccess
{
    public class EmployeeDao : IEmployeeDao
    {
        private readonly IDatabaseFactory _databaseFactory;
        
        public EmployeeDao(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(IEnumerable<int> employeeIds)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            return await connection
                .QueryAsync<EmployeeDto>(
                    "SELECT * FROM project_management.employee WHERE id IN @Ids",
                    new {Ids = employeeIds}
                );
        }
    }
}