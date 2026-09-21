using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;

        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET ALL EMPLOYEES
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        // GET EMPLOYEE BY ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        // ADD EMPLOYEE
        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        // UPDATE EMPLOYEE
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);

            await _context.SaveChangesAsync();
        }

        // DELETE EMPLOYEE
        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
        }
    }
}