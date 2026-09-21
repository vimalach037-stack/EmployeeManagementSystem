using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data;

public class ApplicationDBContext
    : DbContext
{
    public ApplicationDBContext(
        DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Attendance> Attendances { get; set; }

    public DbSet<Salary> Salaries { get; set; }

    public DbSet<Performance> Performances { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==============================
        // USER
        // ==============================

        modelBuilder.Entity<User>()
            .HasIndex(x => x.UserName)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        // ==============================
        // EMPLOYEE
        // ==============================

        modelBuilder.Entity<Employee>()
            .HasIndex(x => x.EmployeeId)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .Property(x => x.Salary)
            .HasPrecision(18, 2);

        // ==============================
        // SALARY
        // ==============================

        modelBuilder.Entity<Salary>()
            .Property(x => x.BasicSalary)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Salary>()
            .Property(x => x.Allowance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Salary>()
            .Property(x => x.Deduction)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Salary>()
            .Property(x => x.NetSalary)
            .HasPrecision(18, 2);

        // ==============================
        // EMPLOYEE → DEPARTMENT
        // ==============================

        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Department)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================
        // EMPLOYEE → ATTENDANCE
        // ==============================

        modelBuilder.Entity<Attendance>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.Attendances)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // ==============================
        // EMPLOYEE → SALARY
        // ==============================

        modelBuilder.Entity<Salary>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.Salaries)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // ==============================
        // EMPLOYEE → PERFORMANCE
        // ==============================

        modelBuilder.Entity<Performance>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.Performances)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}