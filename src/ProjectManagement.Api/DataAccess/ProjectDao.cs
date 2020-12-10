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
        private readonly IDatabaseFactory _databaseFactory;

        public ProjectDao(IDatabaseFactory databaseFactory)
        {
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

        public async Task UpdateAsync(ProjectDto projectDto)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            var sql =
                $@"UPDATE project_management.project SET name = '{projectDto.Name}', state = {projectDto.State},
                    progress = {projectDto.Progress}, owner = {projectDto.Owner}, participant = '{projectDto.Participant}'
                    WHERE id = {projectDto.Id};";

            await connection.ExecuteAsync(sql);
        }
    }
}