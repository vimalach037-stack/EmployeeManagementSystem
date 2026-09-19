using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto dto);

        Task<bool> RegisterAsync(RegisterDto dto);
    }
}