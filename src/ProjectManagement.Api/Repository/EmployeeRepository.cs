using System.Collections.Generic;
using System.Linq;
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
            var employee = _employeeDao.GetEmployeesAsync(new List<int> {employeeId}).ToList();

            return 
                   employee.Count != 0 &&
                   employee.First().GetType() != typeof(NullEmployee) &&
                   employee.First().EmployeeRole == EmployeeRole.TeamManager;
        }

        public Employee GetEmployeeAsync(int employeeId)
        {
            return GetEmployeesAsync(new List<int> {employeeId}).First();
        }

        public IEnumerable<Employee> GetEmployeesAsync(List<int> employeeIds)
        {
            return _employeeDao.GetEmployeesAsync(employeeIds);
        }
    }
}