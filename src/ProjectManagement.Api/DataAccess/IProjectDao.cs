using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IProjectDao
    {
        Task<IEnumerable<Project>> GetAsync();
        
        Task<Project> GetAsync(int projectId);

        Task CreateAsync(ProjectDto project);

        int? UpdateAsync(Project project);
    }
}