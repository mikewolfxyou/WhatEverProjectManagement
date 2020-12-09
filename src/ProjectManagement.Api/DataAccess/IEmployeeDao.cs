using System.Collections.Generic;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IEmployeeDao
    {
        IEnumerable<Employee> GetEmployeesAsync(IEnumerable<int> employeeIds);
    }
}