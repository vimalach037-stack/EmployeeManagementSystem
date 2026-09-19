using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(
            IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();

            return departments.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                Description = d.Description
            }).ToList();
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);

            if (d == null)
                return null;

            return new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                Description = d.Description
            };
        }

        public async Task<DepartmentDto> AddAsync(
            DepartmentDto dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                Description = dto.Description
            };

            var result =
                await _repository.AddAsync(department);

            dto.DepartmentId = result.DepartmentId;

            return dto;
        }

        public async Task<bool> UpdateAsync(
            int id,
            DepartmentDto dto)
        {
            var department =
                await _repository.GetByIdAsync(id);

            if (department == null)
                return false;

            department.DepartmentName =
                dto.DepartmentName;

            department.Description =
                dto.Description;

            await _repository.UpdateAsync(department);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department =
                await _repository.GetByIdAsync(id);

            if (department == null)
                return false;

            await _repository.DeleteAsync(department);

            return true;
        }
    }
}