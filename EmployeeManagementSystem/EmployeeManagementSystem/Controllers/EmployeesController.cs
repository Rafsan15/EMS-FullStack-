using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Interfaces;
using Framework;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeDTOService _employeeDTOService;
        public EmployeesController(IEmployeeService employeeService, IEmployeeDTOService employeeDTOService)
        {
            _employeeService = employeeService;
            _employeeDTOService = employeeDTOService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeDTOService.GetAll();
            if (result.HasError == false)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _employeeDTOService.GetById(id);
            if (result.HasError == false)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
           
            var result = await _employeeDTOService.GetByEmail(email);
            if (result.HasError == false)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee(int id, Employee employee)
        {
            var result = await _employeeService.Save(id, employee);
            if (result.HasError == false)
            {
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
           
            var result = await _employeeService.Save(id, employee);
            if (result.HasError == false)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.Delete(id);
            if (result.HasError == false)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
