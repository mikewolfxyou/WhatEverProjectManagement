using System.Collections.Generic;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Repository
{
    public interface IEmployeeRepository
    {
        bool IsEmployeeTeamManagerAsync(int employeeId);

        Employee GetEmployeeAsync(int employeeId);

        IEnumerable<Employee> GetEmployeesAsync(List<int> employeeIds);
    }
}