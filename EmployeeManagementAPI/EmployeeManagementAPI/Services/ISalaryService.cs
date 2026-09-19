using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface ISalaryService
    {
        Task<List<SalaryDto>> GetAllAsync();

        Task<SalaryDto?> GetByIdAsync(int id);

        Task<List<SalaryDto>> GetByEmployeeIdAsync(int employeeId);

        Task<SalaryDto> AddAsync(SalaryDto dto);

        Task<bool> UpdateAsync(int id, SalaryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}