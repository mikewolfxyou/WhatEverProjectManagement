using System.Threading.Tasks;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IEmployeeRoleDao
    {
        Task<EmployeeRole> GetEmployeeRole(int employeeRoleId);
    }
}