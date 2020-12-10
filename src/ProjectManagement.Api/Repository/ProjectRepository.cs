using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Services.Validator;

namespace ProjectManagement.Api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IProjectDao _projectDao;
        
        private readonly ProjectValidator _projectValidator;

        public ProjectRepository(IProjectDao projectDao, ProjectValidator projectValidator)
        {
            _projectDao = projectDao;
            _projectValidator = projectValidator;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _projectDao.GetAsync();
        }

        public async Task<Project> GetProjectAsync(int projectId)
        {
            return await _projectDao.GetAsync(projectId);
        }

        public async Task CreateProjectAsync(Project project)
        {
            var projectorValidation = _projectValidator.Validate(project);

            if (!projectorValidation.IsValid)
            {
                throw new ArgumentException(projectorValidation.Erros.First().Message);
            }

            await _projectDao.CreateAsync(new ProjectDto
            {
                Name = project.Name,
                Owner = project.OwnerEmployeeId,
                State = (int) project.State,
                Progress = project.Progress,
                Participant = JsonSerializer.Serialize(project.ParticipantEmployeeIds)
            });
        }

        public int? UpdateProjectAsync(Project project)
        {
            var projectorValidation = _projectValidator.Validate(project);

            if (!projectorValidation.IsValid)
            {
                throw new ArgumentException(projectorValidation.Erros.First().Message);
            }

            return _projectDao.UpdateAsync(project);
        }
    }
}