using System.Collections.Generic;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IDepartmentDao
    {
        Dictionary<int, Department> GetDepartmentsAsync();
    }
}