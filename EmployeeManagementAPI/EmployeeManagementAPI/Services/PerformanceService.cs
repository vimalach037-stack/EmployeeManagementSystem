using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository _repository;

        public PerformanceService(
            IPerformanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PerformanceDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(p => new PerformanceDto
            {
                PerformanceId = p.PerformanceId,
                EmployeeId = p.EmployeeId,
                ReviewDate = p.ReviewDate,
                Rating = p.Rating,
                Comments = p.Comments,
                Reviewer = p.Reviewer
            }).ToList();
        }

        public async Task<PerformanceDto?> GetByIdAsync(int id)
        {
            var p = await _repository.GetByIdAsync(id);

            if (p == null)
                return null;

            return new PerformanceDto
            {
                PerformanceId = p.PerformanceId,
                EmployeeId = p.EmployeeId,
                ReviewDate = p.ReviewDate,
                Rating = p.Rating,
                Comments = p.Comments,
                Reviewer = p.Reviewer
            };
        }

        public async Task<List<PerformanceDto>>
            GetByEmployeeIdAsync(int employeeId)
        {
            var data =
                await _repository.GetByEmployeeIdAsync(employeeId);

            return data.Select(p => new PerformanceDto
            {
                PerformanceId = p.PerformanceId,
                EmployeeId = p.EmployeeId,
                ReviewDate = p.ReviewDate,
                Rating = p.Rating,
                Comments = p.Comments,
                Reviewer = p.Reviewer
            }).ToList();
        }

        public async Task<PerformanceDto> AddAsync(
            PerformanceDto dto)
        {
            var performance = new Performance
            {
                EmployeeId = dto.EmployeeId,
                ReviewDate = dto.ReviewDate,
                Rating = dto.Rating,
                Comments = dto.Comments,
                Reviewer = dto.Reviewer
            };

            var result =
                await _repository.AddAsync(performance);

            dto.PerformanceId = result.PerformanceId;

            return dto;
        }

        public async Task<bool> UpdateAsync(
            int id,
            PerformanceDto dto)
        {
            var performance =
                await _repository.GetByIdAsync(id);

            if (performance == null)
                return false;

            performance.EmployeeId = dto.EmployeeId;
            performance.ReviewDate = dto.ReviewDate;
            performance.Rating = dto.Rating;
            performance.Comments = dto.Comments;
            performance.Reviewer = dto.Reviewer;

            await _repository.UpdateAsync(performance);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var performance =
                await _repository.GetByIdAsync(id);

            if (performance == null)
                return false;

            await _repository.DeleteAsync(performance);

            return true;
        }
    }
}