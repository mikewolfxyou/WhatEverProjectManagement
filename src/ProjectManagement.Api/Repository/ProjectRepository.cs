using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProjectRepository(IProjectDao projectDao, ProjectValidator projectValidator)
        {
            _projectDao = projectDao;
            _projectValidator = projectValidator;
        }

        public Dictionary<int, Project> GetProjectsAsync()
        {
            return _projectDao.GetAsync();
        }

        public Project GetProjectAsync(int projectId)
        {
            return _projectDao.GetAsync(projectId);
        }

        public int? CreateProjectAsync(Project project)
        {
            var projectorValidation = _projectValidator.Validate(project);

            if (!projectorValidation.IsValid)
            {
                throw new ArgumentException(projectorValidation.Erros.First().Message);
            }

            return _projectDao.CreateAsync(project);
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