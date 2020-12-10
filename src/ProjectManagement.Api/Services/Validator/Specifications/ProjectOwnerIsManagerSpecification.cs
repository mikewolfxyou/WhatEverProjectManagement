using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Services.Validator.Specifications
{
    public class ProjectOwnerIsManagerSpecification : ISpecification<Project>
    {
        public bool IsSatisfiedBy(Project project)
        {
            return project.Owner.GetType() != typeof(NullEmployee) &&  
                project.Owner.EmployeeRole.Id == EmployeeRepository.TeamManager;
        }
    }
}