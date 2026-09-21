using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;

using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // =========================================================
        // GET ALL EMPLOYEES
        // =========================================================

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Designation = e.Designation,
                JoiningDate = e.JoiningDate,
                Salary = e.Salary,
                DepartmentId = e.DepartmentId
            }).ToList();
        }


        // =========================================================
        // GET EMPLOYEE BY ID
        // =========================================================

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Designation = employee.Designation,
                JoiningDate = employee.JoiningDate,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };
        }


        // =========================================================
        // CREATE EMPLOYEE
        // =========================================================

        public async Task<EmployeeDto> AddAsync(EmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Designation = dto.Designation,
                JoiningDate = dto.JoiningDate,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId,
                Status = "Active"
            };

            var result = await _repository.AddAsync(employee);

            dto.EmployeeId = result.EmployeeId;

            return dto;
        }


        // =========================================================
        // UPDATE EMPLOYEE
        // =========================================================

        public async Task<bool> UpdateAsync(
            int id,
            EmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return false;
            }

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Designation = dto.Designation;
            employee.JoiningDate = dto.JoiningDate;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;

            await _repository.UpdateAsync(employee);

            return true;
        }


        // =========================================================
        // DELETE EMPLOYEE
        // =========================================================

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return false;
            }

            await _repository.DeleteAsync(employee);

            return true;
        }
    }
}