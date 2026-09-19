using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;

        public SalaryService(ISalaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SalaryDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(s => new SalaryDto
            {
                SalaryId = s.SalaryId,
                EmployeeId = s.EmployeeId,
                BasicSalary = s.BasicSalary,
                Allowance = s.Allowance,
                Deduction = s.Deduction,
                NetSalary = s.NetSalary,
                SalaryMonth = s.SalaryMonth
            }).ToList();
        }

        public async Task<SalaryDto?> GetByIdAsync(int id)
        {
            var s = await _repository.GetByIdAsync(id);

            if (s == null)
                return null;

            return new SalaryDto
            {
                SalaryId = s.SalaryId,
                EmployeeId = s.EmployeeId,
                BasicSalary = s.BasicSalary,
                Allowance = s.Allowance,
                Deduction = s.Deduction,
                NetSalary = s.NetSalary,
                SalaryMonth = s.SalaryMonth
            };
        }

        public async Task<List<SalaryDto>>
            GetByEmployeeIdAsync(int employeeId)
        {
            var data =
                await _repository.GetByEmployeeIdAsync(employeeId);

            return data.Select(s => new SalaryDto
            {
                SalaryId = s.SalaryId,
                EmployeeId = s.EmployeeId,
                BasicSalary = s.BasicSalary,
                Allowance = s.Allowance,
                Deduction = s.Deduction,
                NetSalary = s.NetSalary,
                SalaryMonth = s.SalaryMonth
            }).ToList();
        }

        public async Task<SalaryDto> AddAsync(SalaryDto dto)
        {
            // Business calculation
            dto.NetSalary =
                dto.BasicSalary +
                dto.Allowance -
                dto.Deduction;

            var salary = new Salary
            {
                EmployeeId = dto.EmployeeId,
                BasicSalary = dto.BasicSalary,
                Allowance = dto.Allowance,
                Deduction = dto.Deduction,
                NetSalary = dto.NetSalary,
                SalaryMonth = dto.SalaryMonth
            };

            var result =
                await _repository.AddAsync(salary);

            dto.SalaryId = result.SalaryId;

            return dto;
        }

        public async Task<bool> UpdateAsync(
            int id,
            SalaryDto dto)
        {
            var salary =
                await _repository.GetByIdAsync(id);

            if (salary == null)
                return false;

            dto.NetSalary =
                dto.BasicSalary +
                dto.Allowance -
                dto.Deduction;

            salary.EmployeeId = dto.EmployeeId;
            salary.BasicSalary = dto.BasicSalary;
            salary.Allowance = dto.Allowance;
            salary.Deduction = dto.Deduction;
            salary.NetSalary = dto.NetSalary;
            salary.SalaryMonth = dto.SalaryMonth;

            await _repository.UpdateAsync(salary);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salary =
                await _repository.GetByIdAsync(id);

            if (salary == null)
                return false;

            await _repository.DeleteAsync(salary);

            return true;
        }
    }
}