using System.Collections.Generic;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEmployeeDao _employeeDao;

        public EmployeeRepository(IEmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }
        
        public bool IsEmployeeTeamManagerAsync(int employeeId)
        {
            var employee = _employeeDao.GetAsync(employeeId);

            return employee.EmployeeRole == EmployeeRole.TeamManager;
        }

        public Employee GetEmployeeAsync(int employeeId)
        {
            return _employeeDao.GetAsync(employeeId);
        }

        public IEnumerable<Employee> GetEmployeesAsync(List<int> employeeIds)
        {
            return _employeeDao.GetEmployeesAsync(employeeIds);
        }
    }
}