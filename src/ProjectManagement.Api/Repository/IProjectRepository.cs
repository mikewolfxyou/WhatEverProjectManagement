using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();

        Task<Project> GetProjectAsync(int projectId);

        Task CreateProjectAsync(ProjectDto projectDto);

        Task UpdateProjectAsync(ProjectDto projectDto);
    }
}