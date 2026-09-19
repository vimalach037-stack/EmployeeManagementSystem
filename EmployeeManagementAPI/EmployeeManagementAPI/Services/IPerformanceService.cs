using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IPerformanceService
    {
        Task<List<PerformanceDto>> GetAllAsync();

        Task<PerformanceDto?> GetByIdAsync(int id);

        Task<List<PerformanceDto>>
            GetByEmployeeIdAsync(int employeeId);

        Task<PerformanceDto> AddAsync(PerformanceDto dto);

        Task<bool> UpdateAsync(int id, PerformanceDto dto);

        Task<bool> DeleteAsync(int id);
    }
}