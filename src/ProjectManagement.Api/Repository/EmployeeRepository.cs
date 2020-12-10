using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Services;

namespace ProjectManagement.Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public const int TeamManager = 2;

        private readonly IEmployeeDao _employeeDao;

        private readonly IEmployeeFactory _employeeFactory;

        public EmployeeRepository(IEmployeeDao employeeDao, IEmployeeFactory employeeFactory)
        {
            _employeeDao = employeeDao;
            _employeeFactory = employeeFactory;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            var employeeDtos = (await _employeeDao.GetEmployeesAsync(
                new List<int> {employeeId})).ToList();

            return employeeDtos.Count != 0
                ? await _employeeFactory.CreateAsync(employeeDtos.First())
                : new NullEmployee();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(IEnumerable<int> employeeIds)
        {
            var employeeDtos = await _employeeDao.GetEmployeesAsync(employeeIds);
            var employees = new List<Employee>();
            foreach (var employeeDto in employeeDtos)
            {
                employees.Add(await _employeeFactory.CreateAsync(employeeDto));
            }

            return employees;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employeeDtos = await _employeeDao.GetEmployeesAsync();
            
            var employees = new List<Employee>();
            foreach (var employeeDto in employeeDtos)
            {
                employees.Add(await _employeeFactory.CreateAsync(employeeDto));
            }

            return employees;
        }
    }
}