using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly ApplicationDBContext _context;

        public PerformanceRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Performance>> GetAllAsync()
        {
            return await _context.Performances
                .Include(p => p.Employee)
                .ToListAsync();
        }

        public async Task<Performance?> GetByIdAsync(int id)
        {
            return await _context.Performances
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(
                    p => p.PerformanceId == id);
        }

        public async Task<List<Performance>> GetByEmployeeIdAsync(
            int employeeId)
        {
            return await _context.Performances
                .Where(p => p.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<Performance> AddAsync(
            Performance performance)
        {
            await _context.Performances.AddAsync(performance);
            await _context.SaveChangesAsync();

            return performance;
        }

        public async Task UpdateAsync(
            Performance performance)
        {
            _context.Performances.Update(performance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            Performance performance)
        {
            _context.Performances.Remove(performance);
            await _context.SaveChangesAsync();
        }
    }
}