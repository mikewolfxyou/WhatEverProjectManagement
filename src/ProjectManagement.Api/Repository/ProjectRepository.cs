using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Services;
using ProjectManagement.Api.Services.Validator;

namespace ProjectManagement.Api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IProjectDao _projectDao;
        
        private readonly ProjectValidator _projectValidator;

        private readonly IProjectFactory _projectFactory;

        public ProjectRepository(IProjectDao projectDao, ProjectValidator projectValidator, IProjectFactory projectFactory)
        {
            _projectDao = projectDao;
            _projectValidator = projectValidator;
            _projectFactory = projectFactory;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var projectDtos = await _projectDao.GetAsync();
            var projects = new List<Project>();
            foreach (var projectDto in projectDtos)
            {
                projects.Add(await _projectFactory.CreateAsync(projectDto));
            }

            return projects;
        }

        public async Task<Project> GetProjectAsync(int projectId)
        {
            var projectDtos = await _projectDao.GetAsync(projectId);
            var enumerable = projectDtos.ToList();
            if (!enumerable.Any())
            {
                return new NullProject();
            }
            return await _projectFactory.CreateAsync(enumerable.First());
        }

        public async Task CreateProjectAsync(ProjectDto projectDto)
        {
            var projectorValidation = _projectValidator.Validate(await _projectFactory.CreateAsync(projectDto));

            if (!projectorValidation.IsValid)
            {
                throw new ArgumentException(projectorValidation.Erros.First().Message);
            }

            await _projectDao.CreateAsync(projectDto);
        }
        
        public async Task UpdateProjectAsync(ProjectDto projectDto)
        {
            var projectorValidation = _projectValidator.Validate(await _projectFactory.CreateAsync(projectDto));

            if (!projectorValidation.IsValid)
            {
                throw new ArgumentException(projectorValidation.Erros.First().Message);
            }

            await _projectDao.UpdateAsync(projectDto);
        }
    }
}