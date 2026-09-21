
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            ApplicationDBContext db)
        {
            // =====================================================
            // CREATE DATABASE TABLES
            // =====================================================

            await db.Database.EnsureCreatedAsync();


            // =====================================================
            // ADMIN USER
            // =====================================================

            if (!await db.Users.AnyAsync())
            {
                var admin = new User
                {
                    UserName = "admin",

                    Email = "admin@bookexpert.com",

                    PasswordHash =
                        PasswordHelper.HashPassword("Admin@123"),

                    Role = "Admin"
                };

                db.Users.Add(admin);

                await db.SaveChangesAsync();
            }


            // =====================================================
            // DEPARTMENTS
            // =====================================================

            if (!await db.Departments.AnyAsync())
            {
                db.Departments.AddRange(

                    new Department
                    {
                        DepartmentName = "IT",

                        Description =
                            "Information Technology"
                    },

                    new Department
                    {
                        DepartmentName = "HR",

                        Description =
                            "Human Resources"
                    },

                    new Department
                    {
                        DepartmentName = "Finance",

                        Description =
                            "Finance and Accounts"
                    },

                    new Department
                    {
                        DepartmentName = "Sales",

                        Description =
                            "Sales Department"
                    },

                    new Department
                    {
                        DepartmentName = "Marketing",

                        Description =
                            "Marketing Department"
                    }
                );

                await db.SaveChangesAsync();
            }


            // =====================================================
            // EMPLOYEES
            // =====================================================

            if (!await db.Employees.AnyAsync())
            {
                var departments =
                    await db.Departments
                        .ToDictionaryAsync(
                            d => d.DepartmentName,
                            d => d.DepartmentId
                        );


                // Employee 1

                db.Employees.Add(
                    new Employee
                    {
                        FirstName = "Ravi",

                        LastName = "Kumar",

                        Email = "ravi@example.com",

                        Phone = "9876543210",

                        Designation =
                            "Software Developer",

                        JoiningDate =
                            new DateTime(2024, 1, 15),

                        Status = "Active",

                        Salary = 45000,

                        DepartmentId =
                            departments["IT"]
                    }
                );


                // Employee 2

                db.Employees.Add(
                    new Employee
                    {
                        FirstName = "Priya",

                        LastName = "Sharma",

                        Email = "priya@example.com",

                        Phone = "9876543211",

                        Designation =
                            "HR Executive",

                        JoiningDate =
                            new DateTime(2023, 6, 1),

                        Status = "Active",

                        Salary = 40000,

                        DepartmentId =
                            departments["HR"]
                    }
                );


                // Employee 3

                db.Employees.Add(
                    new Employee
                    {
                        FirstName = "Suresh",

                        LastName = "Rao",

                        Email = "suresh@example.com",

                        Phone = "9876543212",

                        Designation =
                            "Accountant",

                        JoiningDate =
                            new DateTime(2022, 3, 10),

                        Status = "Active",

                        Salary = 42000,

                        DepartmentId =
                            departments["Finance"]
                    }
                );


                await db.SaveChangesAsync();
            }


            // =====================================================
            // ATTENDANCE
            // =====================================================

            if (!await db.Attendances.AnyAsync())
            {
                var employees =
                    await db.Employees
                        .ToListAsync();


                foreach (var employee in employees)
                {
                    var attendance =
                        new Attendance
                        {
                            EmployeeId =
                                employee.EmployeeId,

                            AttendanceDate =
                                DateTime.Today,

                            Status =
                                "Present",

                            CheckIn =
                                new TimeSpan(
                                    9,
                                    0,
                                    0
                                ),

                            CheckOut =
                                new TimeSpan(
                                    18,
                                    0,
                                    0
                                )
                        };


                    db.Attendances.Add(
                        attendance
                    );
                }


                await db.SaveChangesAsync();
            }


            // =====================================================
            // SALARY
            // =====================================================

            if (!await db.Salaries.AnyAsync())
            {
                var employees =
                    await db.Employees
                        .ToListAsync();


                foreach (var employee in employees)
                {
                    var salary =
                        new Salary
                        {
                            EmployeeId =
                                employee.EmployeeId,

                            BasicSalary =
                                employee.Salary * 0.80m,

                            Allowance =
                                employee.Salary * 0.20m,

                            Deduction = 0,

                            NetSalary =
                                employee.Salary
                        };


                    db.Salaries.Add(
                        salary
                    );
                }


                await db.SaveChangesAsync();
            }


            // =====================================================
            // PERFORMANCE
            // =====================================================

            if (!await db.Performances.AnyAsync())
            {
                var employees =
                    await db.Employees
                        .ToListAsync();


                int rating = 3;


                foreach (var employee in employees)
                {
                    var performance =
                        new Performance
                        {
                            EmployeeId =
                                employee.EmployeeId,

                            ReviewDate =
                                DateTime.Today,

                            Rating =
                                Math.Min(
                                    rating,
                                    5
                                ),

                            Comments =
                                "Initial assessment"
                        };


                    db.Performances.Add(
                        performance
                    );


                    rating++;
                }


                await db.SaveChangesAsync();
            }
        }
    }
}