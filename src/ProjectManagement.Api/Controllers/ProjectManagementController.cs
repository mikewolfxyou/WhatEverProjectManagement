using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("project-management")]
    public class ProjectManagementController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        
        public ProjectManagementController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpPost("project")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _projectRepository.CreateProjectAsync(project);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Accepted();
        }
        
        [HttpPut("project/{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Update(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _projectRepository.GetProjectAsync((int) project.Id) is NullProject)
            {
                return NotFound();
            }

            try
            {
                _projectRepository.UpdateProjectAsync(project);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Accepted();
        }
        
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Get()
        {
            var projects = await _projectRepository.GetProjectsAsync();
            return JsonSerializer.Serialize(projects);
        }
    }
}