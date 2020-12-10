using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Services
{
    public class ProjectFactory : IProjectFactory
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectFactory(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Project> CreateAsync(ProjectDto projectDto)
        {
            return new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                State = (ProjectState) projectDto.State,
                Progress = projectDto.Progress,
                Owner = await _employeeRepository.GetEmployeeAsync(projectDto.Owner),
                Participants = await _employeeRepository.GetEmployeesAsync(
                    JsonSerializer.Deserialize<List<int>>(projectDto.Participant))
            };
        }
    }
}