using System.Collections.Generic;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Repository
{
    public interface IProjectRepository
    {
        Dictionary<int, Project> GetProjectsAsync();

        Project GetProjectAsync(int projectId);

        int? CreateProjectAsync(Project project);

        int? UpdateProjectAsync(Project project);
    }
}