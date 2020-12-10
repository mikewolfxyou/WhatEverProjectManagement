using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ProjectManagement.Api.Infrastructure;

namespace ProjectManagement.Api.DataAccess
{
    public class ProjectDao : IProjectDao
    {
        private readonly IDatabaseFactory _databaseFactory;

        public ProjectDao(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<IEnumerable<ProjectDto>> GetAsync()
        {
            await using var connection = await _databaseFactory.CreateConnection();
            return await connection
                    .QueryAsync<ProjectDto>("SELECT * FROM project_management.project");
        }

        public async Task<IEnumerable<ProjectDto>> GetAsync(int projectId)
        {
            await using var connection = await _databaseFactory.CreateConnection();
            return await connection
                .QueryAsync<ProjectDto>(
                    @"SELECT * FROM project_management.project WHERE id = @Id",
                    new {Id = projectId}
                );
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