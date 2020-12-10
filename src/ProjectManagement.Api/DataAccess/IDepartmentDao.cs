using System.Threading.Tasks;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IDepartmentDao
    {
        Task<Department> GetDepartmentAsync(int departmentId);
    }
}