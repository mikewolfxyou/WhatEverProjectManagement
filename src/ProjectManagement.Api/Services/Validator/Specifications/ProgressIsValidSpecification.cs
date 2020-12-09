using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services.Validator.Specifications
{
    public class ProgressIsValidSpecification : ISpecification<Project>
    {
        public bool IsSatisfiedBy(Project project)
        {
            return project.Progress >= 0 && project.Progress <= 100;
        }
    }
}