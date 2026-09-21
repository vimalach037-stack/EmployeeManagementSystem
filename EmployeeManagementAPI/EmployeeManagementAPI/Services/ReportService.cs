using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<object> GetEmployeeSummaryAsync()
        {
            return await _reportRepository.GetEmployeeSummaryAsync();
        }

        public async Task<object> GetDepartmentSummaryAsync()
        {
            return await _reportRepository.GetDepartmentSummaryAsync();
        }

        public async Task<object> GetAttendanceSummaryAsync()
        {
            return await _reportRepository.GetAttendanceSummaryAsync();
        }

        public async Task<object> GetSalarySummaryAsync()
        {
            return await _reportRepository.GetSalarySummaryAsync();
        }

        public async Task<object> GetPerformanceSummaryAsync()
        {
            return await _reportRepository.GetPerformanceSummaryAsync();
        }
    }
}