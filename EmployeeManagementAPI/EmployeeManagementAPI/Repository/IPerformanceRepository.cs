using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public interface IPerformanceRepository
    {
        Task<List<Performance>> GetAllAsync();

        Task<Performance?> GetByIdAsync(int id);

        Task<List<Performance>> GetByEmployeeIdAsync(int employeeId);

        Task<Performance> AddAsync(Performance performance);

        Task UpdateAsync(Performance performance);

        Task DeleteAsync(Performance performance);
    }
}