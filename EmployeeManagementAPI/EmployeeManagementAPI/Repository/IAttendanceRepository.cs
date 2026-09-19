using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task<List<Attendance>> GetByEmployeeIdAsync(int employeeId);

        Task<Attendance> AddAsync(Attendance attendance);

        Task UpdateAsync(Attendance attendance);

        Task DeleteAsync(Attendance attendance);
    }
}