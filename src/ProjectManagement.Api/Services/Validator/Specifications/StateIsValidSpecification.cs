using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services.Validator.Specifications
{
    public class StateIsValidSpecification : ISpecification<Project>
    {
        public bool IsSatisfiedBy(Project project)
        {
            return project.State == ProjectState.Active ||
                   project.State == ProjectState.Done ||
                   project.State == ProjectState.Failed ||
                   project.State == ProjectState.Planned;
        }
    }
}