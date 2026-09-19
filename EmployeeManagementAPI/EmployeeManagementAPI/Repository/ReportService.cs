using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> GetEmployeeSummaryAsync()
        {
            return await _repository
                .GetEmployeeSummaryAsync();
        }

        public async Task<object> GetDepartmentSummaryAsync()
        {
            return await _repository
                .GetDepartmentSummaryAsync();
        }

        public async Task<object> GetAttendanceSummaryAsync()
        {
            return await _repository
                .GetAttendanceSummaryAsync();
        }

        public async Task<object> GetSalarySummaryAsync()
        {
            return await _repository
                .GetSalarySummaryAsync();
        }

        public async Task<object> GetPerformanceSummaryAsync()
        {
            return await _repository
                .GetPerformanceSummaryAsync();
        }
    }
}