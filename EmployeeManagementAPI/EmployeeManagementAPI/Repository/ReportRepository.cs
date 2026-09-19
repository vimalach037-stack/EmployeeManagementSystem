using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDBContext _context;

        public ReportRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<object> GetEmployeeSummaryAsync()
        {
            return new
            {
                TotalEmployees =
                    await _context.Employees.CountAsync(),

                TotalDepartments =
                    await _context.Departments.CountAsync()
            };
        }

        public async Task<object> GetDepartmentSummaryAsync()
        {
            return await _context.Departments
                .Select(d => new
                {
                    DepartmentId = d.DepartmentId,

                    DepartmentName = d.DepartmentName,

                    EmployeeCount =
                        d.Employees.Count()
                })
                .ToListAsync();
        }

        public async Task<object> GetAttendanceSummaryAsync()
        {
            return await _context.Attendances
                .GroupBy(a => a.Status)
                .Select(g => new
                {
                    Status = g.Key,

                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<object> GetSalarySummaryAsync()
        {
            return new
            {
                TotalSalary =
                    await _context.Salaries
                        .SumAsync(s => s.NetSalary),

                AverageSalary =
                    await _context.Salaries
                        .AverageAsync(s => s.NetSalary)
            };
        }

        public async Task<object> GetPerformanceSummaryAsync()
        {
            return await _context.Performances
                .GroupBy(p => p.Rating)
                .Select(g => new
                {
                    Rating = g.Key,

                    EmployeeCount = g.Count()
                })
                .OrderBy(x => x.Rating)
                .ToListAsync();
        }
    }
}