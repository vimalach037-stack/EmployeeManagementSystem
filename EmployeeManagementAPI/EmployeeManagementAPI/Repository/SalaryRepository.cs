using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly ApplicationDBContext _context;

        public SalaryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Salary>> GetAllAsync()
        {
            return await _context.Salaries
                .Include(s => s.Employee)
                .ToListAsync();
        }

        public async Task<Salary?> GetByIdAsync(int id)
        {
            return await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.SalaryId == id);
        }

        public async Task<List<Salary>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Salaries
                .Where(s => s.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<Salary> AddAsync(Salary salary)
        {
            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();

            return salary;
        }

        public async Task UpdateAsync(Salary salary)
        {
            _context.Salaries.Update(salary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Salary salary)
        {
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
        }
    }
}