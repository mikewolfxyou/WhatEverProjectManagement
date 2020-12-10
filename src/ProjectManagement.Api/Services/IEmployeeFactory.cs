using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services
{
    public interface IEmployeeFactory
    {
        Task<Employee> CreateAsync(EmployeeDto employeeDto);
    }
}