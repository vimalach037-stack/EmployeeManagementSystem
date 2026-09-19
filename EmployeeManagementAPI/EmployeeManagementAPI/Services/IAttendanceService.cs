using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<List<AttendanceDto>> GetAllAsync();

        Task<AttendanceDto?> GetByIdAsync(int id);

        Task<List<AttendanceDto>> GetByEmployeeIdAsync(int employeeId);

        Task<AttendanceDto> AddAsync(AttendanceDto dto);

        Task<bool> UpdateAsync(int id, AttendanceDto dto);

        Task<bool> DeleteAsync(int id);
    }
}