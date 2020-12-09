using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Services.Validator.Specifications
{
    public class ProjectOwnerIsManagerSpecification : ISpecification<Project>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectOwnerIsManagerSpecification(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool IsSatisfiedBy(Project project)
        {
            return _employeeRepository.IsEmployeeTeamManagerAsync(project.OwnerEmployeeId);
        }
    }
}