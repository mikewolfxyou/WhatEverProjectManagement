using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Api.DataAccess
{
    public interface IEmployeeDao
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(IEnumerable<int> employeeIds);
    }
}