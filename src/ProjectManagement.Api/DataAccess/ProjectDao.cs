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
                    .QueryAsync<(int Id, string Name, int State, float Progress, int Owner, string Participants)>(
                        "SELECT * FROM project_management.project");

            return result.Select(obj => new Project
            {
                Id = obj.Id,
                Name = obj.Name,
                State = (ProjectState) obj.State,
                Progress = obj.Progress,
                OwnerEmployeeId = obj.Owner,
                ParticipantEmployeeIds = JsonSerializer.Deserialize<List<int>>(obj.Participants)
            });
        }

        public async Task<Project> GetAsync(int projectId)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            var result = await connection
                .QueryAsync<(int Id, string Name, int State, float Progress, int Owner, string Participants)>(
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
                ParticipantEmployeeIds = JsonSerializer.Deserialize<List<int>>(obj.Participants)
            }).ToList();

            return !enumerable.Any() ? new NullProject() : enumerable.First();
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