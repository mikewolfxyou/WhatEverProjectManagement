using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(int employeeId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(IEnumerable<int> employeeIds);
        
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}