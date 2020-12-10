using System.Linq;
using DomainValidation.Interfaces.Specification;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services.Validator.Specifications
{
    public class ParticipantInSameDepartmentOfOwnerSpecification : ISpecification<Project>
    {
        public bool IsSatisfiedBy(Project project)
        {
            var participantDepartments = project.Participants.Select(
                participant => participant.Department
            );
            
            return participantDepartments.All(department => department.Equals(project.Owner.Department));
        }
    }
}