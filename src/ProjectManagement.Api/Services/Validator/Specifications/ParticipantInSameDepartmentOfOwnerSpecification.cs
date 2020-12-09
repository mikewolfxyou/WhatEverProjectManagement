using System.Linq;
using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Services.Validator.Specifications
{
    public class ParticipantInSameDepartmentOfOwnerSpecification : ISpecification<Project>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ParticipantInSameDepartmentOfOwnerSpecification(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool IsSatisfiedBy(Project project)
        {
            var participantDepartmentIds = _employeeRepository.GetEmployeesAsync(project.ParticipantEmployeeIds)
                .Select(employee => employee.DepartmentId);
            var owner = _employeeRepository.GetEmployeeAsync(project.OwnerEmployeeId);

            return participantDepartmentIds.All(departmentId => departmentId == owner.DepartmentId);
        }
    }
}