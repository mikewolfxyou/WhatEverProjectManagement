using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Api.DataAccess
{
    public interface IProjectDao
    {
        Task<IEnumerable<ProjectDto>> GetAsync();
        
        Task<IEnumerable<ProjectDto>> GetAsync(int projectId);

        Task CreateAsync(ProjectDto project);

        Task UpdateAsync(ProjectDto project);
    }
}