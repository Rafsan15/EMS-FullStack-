using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _departmentService.GetAll();
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
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _departmentService.GetById(id);
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
        public async Task<IActionResult> CreateEmployee(int id, Department department)
        {
            var result = await _departmentService.Save(id, department);
            if (result.HasError == false)
            {
                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Department department)
        {

            var result = await _departmentService.Save(id, department);
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
            var result = await _departmentService.Delete(id);
            if (result.HasError == false)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
