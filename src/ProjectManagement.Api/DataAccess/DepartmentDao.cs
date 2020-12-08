using System.Collections.Generic;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public class DepartmentDao : IDepartmentDao
    {
        private readonly Dictionary<int, Department> _departments;

        public DepartmentDao()
        {
            _departments = new Dictionary<int, Department>
            {
                [1] = new Department
                {
                    Id = 1,
                    Name = "Search Tech"
                },
                [2] = new Department
                {
                    Id = 2,
                    Name = "Front store"
                },
            };
        }


        public Dictionary<int, Department> GetDepartmentsAsync()
        {
            return _departments;
        }
    }
}