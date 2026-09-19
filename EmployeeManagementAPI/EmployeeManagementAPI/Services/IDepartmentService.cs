using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto?> GetByIdAsync(int id);

        Task<DepartmentDto> AddAsync(DepartmentDto dto);

        Task<bool> UpdateAsync(int id, DepartmentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}