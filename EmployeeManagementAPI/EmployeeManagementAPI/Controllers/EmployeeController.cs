using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // =====================================================
        // GET: api/Employee
        // =====================================================
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            return Ok(employees);
        }


        // =====================================================
        // GET: api/Employee/1
        // =====================================================
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Employee ID must be greater than 0."
                });
            }

            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new
                {
                    message = $"Employee with ID {id} not found."
                });
            }

            return Ok(employee);
        }


        // =====================================================
        // POST: api/Employee
        // =====================================================
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateEmployee(
            [FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEmployee = await _employeeService.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }


        // =====================================================
        // PUT: api/Employee/1
        // =====================================================
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            [FromBody] EmployeeDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Employee ID must be greater than 0."
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateAsync(id, dto);

            if (!result)
            {
                return NotFound(new
                {
                    message = $"Employee with ID {id} not found."
                });
            }

            return Ok(new
            {
                message = "Employee updated successfully.",
                employeeId = id
            });
        }


        // =====================================================
        // DELETE: api/Employee/1
        // =====================================================
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Employee ID must be greater than 0."
                });
            }

            var result = await _employeeService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = $"Employee with ID {id} not found."
                });
            }

            return Ok(new
            {
                message = "Employee deleted successfully."
            });
        }
    }
}