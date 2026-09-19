using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> GetByIdAsync(int userId);

        Task<User?> GetByUsernameAsync(string username);

        Task<User> AddAsync(User user);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(int userId);
    }
}