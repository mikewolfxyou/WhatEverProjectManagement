using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public interface IProjectDao
    {
        Task<IEnumerable<Project>> GetAsync();
        
        Project GetAsync(int projectId);

        int? CreateAsync(Project project);

        int? UpdateAsync(Project project);
    }
}