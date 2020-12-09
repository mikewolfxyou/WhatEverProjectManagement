using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services
{
    public class ProgressIsValidSpecification : ISpecification<Project>
    {
        public bool IsSatisfiedBy(Project project)
        {
            return project.Progress >= 0 && project.Progress <= 100;
        }
    }
}