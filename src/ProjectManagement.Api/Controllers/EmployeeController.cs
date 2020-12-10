using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("employee-query")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        [HttpGet("employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IStatusCodeActionResult> Get()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesAsync();
                return Ok(await JsonConvert.SerializeObjectAsync(employees));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        
        [HttpGet("employee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IStatusCodeActionResult> GetById(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeAsync(employeeId);
                if (employee is NullEmployee)
                {
                    return NotFound();
                }
                return Ok(await JsonConvert.SerializeObjectAsync(employee));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}