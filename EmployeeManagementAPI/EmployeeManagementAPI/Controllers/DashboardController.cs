using EmployeeManagementAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DashboardController(
            ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            var today = DateTime.Today;

            var totalEmployees =
                await _context.Employees.CountAsync();

            var totalDepartments =
                await _context.Departments.CountAsync();

            var presentToday =
                await _context.Attendances
                    .CountAsync(a =>
                        a.AttendanceDate.Date == today
                        &&
                        a.Status == "Present");

            var absentToday =
                await _context.Attendances
                    .CountAsync(a =>
                        a.AttendanceDate.Date == today
                        &&
                        a.Status == "Absent");

            var leaveToday =
                await _context.Attendances
                    .CountAsync(a =>
                        a.AttendanceDate.Date == today
                        &&
                        a.Status == "Leave");

            var totalSalary =
                await _context.Employees
                    .Where(e =>
                        e.Status == "Active")
                    .SumAsync(e => e.Salary);

            return Ok(new
            {
                totalEmployees,
                totalDepartments,
                presentToday,
                absentToday,
                leaveToday,
                totalSalary
            });
        }

        [HttpGet("department-summary")]
        public async Task<IActionResult>
            DepartmentSummary()
        {
            var data =
                await _context.Departments
                    .Select(d => new
                    {
                        department =
                            d.DepartmentName,

                        employees =
                            d.Employees.Count
                    })
                    .ToListAsync();

            return Ok(data);
        }

        [HttpGet("hiring-trend")]
        public async Task<IActionResult>
            HiringTrend()
        {
            var data =
                await _context.Employees
                    .GroupBy(e =>
                        new
                        {
                            Year =
                                e.JoiningDate.Year,

                            Month =
                                e.JoiningDate.Month
                        })
                    .Select(g => new
                    {
                        year = g.Key.Year,
                        month = g.Key.Month,
                        hires = g.Count()
                    })
                    .OrderBy(x => x.year)
                    .ThenBy(x => x.month)
                    .ToListAsync();

            return Ok(data);
        }
    }
}