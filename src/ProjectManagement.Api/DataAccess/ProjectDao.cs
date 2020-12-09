using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Api.Infrastructure;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.DataAccess
{
    public class ProjectDao : IProjectDao
    {
        private Dictionary<int, Project> _projects;

        private readonly IDatabaseFactory _databaseFactory;

        public ProjectDao(IDatabaseFactory databaseFactory)
        {
            _projects = new Dictionary<int, Project>
            {
                [1] = new Project
                {
                    Id = 1,
                    Name = "Project 1 - belongs to search tech",
                    State = ProjectState.Active,
                    OwnerEmployeeId = 4,
                    ParticipantEmployeeIds = new List<int> {1}
                },
                [2] = new Project
                {
                    Id = 2,
                    Name = "Project 2 - belongs to front store",
                    State = ProjectState.Done,
                    OwnerEmployeeId = 3,
                    ParticipantEmployeeIds = new List<int> {2}
                },
            };

            _databaseFactory = databaseFactory;
            var a = 1;
        }

        public Dictionary<int, Project> GetAsync()
        {
            return _projects;
        }

        public Project GetAsync(int projectId)
        {
            return !_projects.ContainsKey(projectId) ? new NullProject() : _projects[projectId];
        }

        public int? CreateAsync(Project project)
        {
            project.Id ??= _projects.Last().Key + 1;
            _projects.Add((int) project.Id, new Project
            {
                Id = project.Id,
                Name = project.Name,
                State = project.State,
                OwnerEmployeeId = project.OwnerEmployeeId,
                ParticipantEmployeeIds = project.ParticipantEmployeeIds
            });
            
            return project.Id;
        }

        public int? UpdateAsync(Project project)
        {
            if (project.Id == null) throw new ArgumentException("Update project: project id is null");
            _projects[(int) project.Id] = project;

            return project.Id;
        }
    }
}