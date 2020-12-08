using System.Collections.Generic;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IProjectDao
    {
        Dictionary<int, Project> GetAsync();
        
        Project GetAsync(int projectId);

        int? CreateAsync(Project project);

        int? UpdateAsync(Project project);
    }
}