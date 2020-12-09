using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Repository
{
    public interface IProjectRepository
    {
        Task<Dictionary<int, Project>> GetProjectsAsync();

        Project GetProjectAsync(int projectId);

        int? CreateProjectAsync(Project project);

        int? UpdateProjectAsync(Project project);
    }
}