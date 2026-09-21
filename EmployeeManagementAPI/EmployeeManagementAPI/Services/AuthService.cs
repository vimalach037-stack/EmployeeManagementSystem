using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }


        // ============================================================
        // REGISTER
        // ============================================================

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser =
                await _userRepository.GetByUsernameAsync(dto.Username);

            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(dto.Password),

                Role = string.IsNullOrWhiteSpace(dto.Role)
                    ? "admin"
                    : dto.Role
            };

            await _userRepository.AddAsync(user);

            return true;
        }


        // ============================================================
        // LOGIN
        // ============================================================

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user =
                await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
            {
                return null;
            }

            var passwordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash
                );

            if (!passwordValid)
            {
                return null;
            }

            return GenerateToken(user);
        }


        // ============================================================
        // GENERATE JWT TOKEN
        // ============================================================

        private string GenerateToken(User user)
        {
            var jwtKey =
                _configuration["Jwt:Key"];

            var jwtIssuer =
                _configuration["Jwt:Issuer"];

            var jwtAudience =
                _configuration["Jwt:Audience"];


            // Check JWT configuration
            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "Jwt:Key is missing in appsettings.json."
                );
            }

            if (string.IsNullOrWhiteSpace(jwtIssuer))
            {
                throw new InvalidOperationException(
                    "Jwt:Issuer is missing in appsettings.json."
                );
            }

            if (string.IsNullOrWhiteSpace(jwtAudience))
            {
                throw new InvalidOperationException(
                    "Jwt:Audience is missing in appsettings.json."
                );
            }


            // Claims
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.UserId.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role ?? "admin"
                )
            };


            // Secret key
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                );


            // Signing credentials
            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );


            // Token
            var token =
                new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: credentials
                );


            // Convert token to string
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}