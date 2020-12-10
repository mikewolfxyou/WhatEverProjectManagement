using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
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
        }

        public async Task<IEnumerable<Project>> GetAsync()
        {
            await using var connection = await _databaseFactory.CreateConnection();
            var result =
                await connection
                    .QueryAsync<ProjectDto>("SELECT * FROM project_management.project");

            return result.Select(obj => new Project
            {
                Id = obj.Id,
                Name = obj.Name,
                State = (ProjectState) obj.State,
                Progress = obj.Progress,
                OwnerEmployeeId = obj.Owner,
                ParticipantEmployeeIds = JsonSerializer.Deserialize<List<int>>(obj.Participant)
            });
        }

        public async Task<Project> GetAsync(int projectId)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            var result = await connection
                .QueryAsync<ProjectDto>(
                    "SELECT * FROM project_management.project WHERE id = @Id",
                    new {Id = projectId}
                );

            var enumerable = result.Select(obj => new Project
            {
                Id = obj.Id,
                Name = obj.Name,
                State = (ProjectState) obj.State,
                Progress = obj.Progress,
                OwnerEmployeeId = obj.Owner,
                ParticipantEmployeeIds = JsonSerializer.Deserialize<List<int>>(obj.Participant)
            }).ToList();

            return !enumerable.Any() ? new NullProject() : enumerable.First();
        }

        public async Task CreateAsync(ProjectDto projectDto)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            var sql =
                $@"INSERT INTO project_management.project(name, state, progress, owner, participant) 
                    values('{projectDto.Name}', {projectDto.State}, {projectDto.Progress}, {projectDto.Owner},
                    '{projectDto.Participant}'
                )";

            await connection.ExecuteAsync(sql);
        }

        public int? UpdateAsync(Project project)
        {
            if (project.Id == null) throw new ArgumentException("Update project: project id is null");
            _projects[(int) project.Id] = project;

            return project.Id;
        }
    }
}