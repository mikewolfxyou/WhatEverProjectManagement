using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Services
{
    public interface IProjectFactory
    {
        Task<Project> CreateAsync(ProjectDto projectDto);
    }
}