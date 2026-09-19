using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // Users table
        public DbSet<User> Users { get; set; }

        // Employees table
        public DbSet<Employee> Employees { get; set; }

        // Departments table
        public DbSet<Department> Departments { get; set; }

        // Attendance table
        public DbSet<Attendance> Attendances { get; set; }

        // Salary table
        public DbSet<Salary> Salaries { get; set; }

        public DbSet<Performance> Performances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // -----------------------------------------
            // Employee -> Department
            // -----------------------------------------

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            // -----------------------------------------
            // Attendance -> Employee
            // -----------------------------------------

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // -----------------------------------------
            // Salary -> Employee
            // -----------------------------------------

            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}