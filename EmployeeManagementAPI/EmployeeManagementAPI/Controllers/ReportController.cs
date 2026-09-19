using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("employee-summary")]
        public async Task<IActionResult> EmployeeSummary()
        {
            return Ok(
                await _service.GetEmployeeSummaryAsync());
        }

        [HttpGet("department-summary")]
        public async Task<IActionResult> DepartmentSummary()
        {
            return Ok(
                await _service.GetDepartmentSummaryAsync());
        }

        [HttpGet("attendance-summary")]
        public async Task<IActionResult> AttendanceSummary()
        {
            return Ok(
                await _service.GetAttendanceSummaryAsync());
        }

        [HttpGet("salary-summary")]
        public async Task<IActionResult> SalarySummary()
        {
            return Ok(
                await _service.GetSalarySummaryAsync());
        }

        [HttpGet("performance-summary")]
        public async Task<IActionResult> PerformanceSummary()
        {
            return Ok(
                await _service.GetPerformanceSummaryAsync());
        }
    }
}