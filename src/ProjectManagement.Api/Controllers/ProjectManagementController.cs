using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using ProjectManagement.Api.DataAccess;
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
        public async Task<IStatusCodeActionResult> Create(ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _projectRepository.CreateProjectAsync(projectDto);
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
        public async Task<IStatusCodeActionResult> Update(ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _projectRepository.GetProjectAsync(projectDto.Id) is NullProject)
            {
                return NotFound();
            }

            try
            {
                await _projectRepository.UpdateProjectAsync(projectDto);
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IStatusCodeActionResult> Get()
        {
            try
            {
                return Ok(await JsonConvert.SerializeObjectAsync(await _projectRepository.GetProjectsAsync()));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}