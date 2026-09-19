namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IReportService
    {
        Task<object> GetEmployeeSummaryAsync();

        Task<object> GetDepartmentSummaryAsync();

        Task<object> GetAttendanceSummaryAsync();

        Task<object> GetSalarySummaryAsync();

        Task<object> GetPerformanceSummaryAsync();
    }
}