using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IDepartmentDao _departmentDao;
        private readonly IEmployeeRoleDao _employeeRoleDao;

        public EmployeeFactory(IDepartmentDao departmentDao, IEmployeeRoleDao employeeRoleDao)
        {
            _departmentDao = departmentDao;
            _employeeRoleDao = employeeRoleDao;
        }

        public async Task<Employee> CreateAsync(EmployeeDto employeeDto)
        {
            return new Employee
            {
                Id = employeeDto.Id,
                Name = employeeDto.FirstName + " " + employeeDto.LastName,
                Department = await _departmentDao.GetDepartmentAsync(employeeDto.DepartmentId),
                EmployeeRole = await _employeeRoleDao.GetEmployeeRole(employeeDto.EmployeeRoleId),
            };
        }
    }
}