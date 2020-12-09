using DomainValidation.Validation;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Services
{
    public class ProjectValidator : Validator<Project>
    {
        public ProjectValidator(IEmployeeRepository employeeRepository)
        {
            Add("OwnerIsManager",
                new Rule<Project>(new ProjectOwnerIsManagerSpecification(employeeRepository),
                    "Project owner is not manager"));
            Add("ParticipantAreSameDepartmentManager",
                new Rule<Project>(new ParticipantInSameDepartmentOfOwnerSpecification(employeeRepository),
                    "Participant is not in the same department of owner"));
            Add("StateIsValid",
                new Rule<Project>(new StateIsValidSpecification(),
                    "Project state is invalid, could be only Planned - 0, Active - 1, Done - 2, Failed - 3"));

            Add("ProgressIsValid",
                new Rule<Project>(new ProgressIsValidSpecification(), "Progress must be between 0 and 100"));
        }
    }
}