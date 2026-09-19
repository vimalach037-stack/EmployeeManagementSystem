using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();

        Task<EmployeeDto?> GetByIdAsync(int id);

        Task<EmployeeDto> AddAsync(EmployeeDto dto);

        Task<bool> UpdateAsync(int id, EmployeeDto dto);

        Task<bool> DeleteAsync(int id);
    }
}