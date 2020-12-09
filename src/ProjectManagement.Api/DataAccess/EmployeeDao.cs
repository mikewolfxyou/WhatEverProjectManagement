using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public class EmployeeDao : IEmployeeDao
    {
        private Dictionary<int, Employee> _employees;

        public EmployeeDao()
        {
            _employees = new Dictionary<int, Employee>
            {
                [1] = new Employee
                {
                    Id = 1,
                    Name = "Mike",
                    EmployeeRole = EmployeeRole.SoftwareEngineer,
                    DepartmentId = 1
                },
                [2] = new Employee
                {
                    Id = 2,
                    Name = "Tom",
                    EmployeeRole = EmployeeRole.SoftwareEngineer,
                    DepartmentId = 2
                },
                [3] = new Employee
                {
                    Id = 3,
                    Name = "Jerry",
                    EmployeeRole = EmployeeRole.TeamManager,
                    DepartmentId = 2
                },
                [4] = new Employee
                {
                    Id = 4,
                    Name = "Peter",
                    EmployeeRole = EmployeeRole.TeamManager,
                    DepartmentId = 1
                },
            };
        }
        
        public IEnumerable<Employee> GetEmployeesAsync(IEnumerable<int> employeeIds)
        {
            return _employees
                .Where(employee => employeeIds.Contains(employee.Value.Id))
                .Select(employee => employee.Value);
        }
    }
}