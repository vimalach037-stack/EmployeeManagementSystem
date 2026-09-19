using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public interface ISalaryRepository
    {
        Task<List<Salary>> GetAllAsync();

        Task<Salary?> GetByIdAsync(int id);

        Task<List<Salary>> GetByEmployeeIdAsync(int employeeId);

        Task<Salary> AddAsync(Salary salary);

        Task UpdateAsync(Salary salary);

        Task DeleteAsync(Salary salary);
    }
}