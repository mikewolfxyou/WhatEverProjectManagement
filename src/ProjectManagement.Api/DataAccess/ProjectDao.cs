using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProjectManagement.Api.Infrastructure;
using ProjectManagement.Api.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            await using var cmd = new MySqlCommand(@"SELECT * FROM project_management.project", connection);

            var rdr = await cmd.ExecuteReaderAsync();

            var projects = new List<Project>();
            while (await rdr.ReadAsync())
            {
                projects.Add(new Project
                {
                    Id = rdr.GetInt32(0),
                    Name = rdr.GetString(1),
                    OwnerEmployeeId = rdr.GetInt32(2),
                    ParticipantEmployeeIds = JsonSerializer.Deserialize<List<int>>(rdr.GetString(3))
                });
            }

            return projects;
        }

        public async Task<Project> GetAsync(int projectId)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            await using var cmd = new MySqlCommand(
                @"SELECT * FROM project_management.project WHERE id = @id", connection);

            cmd.Parameters.AddWithValue("@id", projectId);
            var rdr = await cmd.ExecuteReaderAsync();

            var projects = new List<Project>();
            while (await rdr.ReadAsync())
            {
                projects.Add(new Project
                {
                    Id = rdr.GetInt32(0),
                    Name = rdr.GetString(1),
                    OwnerEmployeeId = rdr.GetInt32(2),
                    ParticipantEmployeeIds = JsonSerializer.Deserialize<List<int>>(rdr.GetString(3))
                });
            }

            return projects.Count == 0 ? new NullProject() : projects.First();
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