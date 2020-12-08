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
                    "Participant is not in same department of owner"));
            Add("StateIsValid", new Rule<Project>(new IsStateValid(), "Project state is invalid, could be only 0 - Planned, 1 - Active, 2 - Done, 3 - Failed"));
         
            Add("ProgressIsValid", new Rule<Project>(new IsProgressValid(), "Progress must be between 0 and 100"));
        }
    }
}