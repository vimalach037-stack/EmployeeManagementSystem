namespace EmployeeManagementAPI.Models
{
    public class Salary
    {
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowance { get; set; }

        public decimal Deduction { get; set; }

        public decimal NetSalary { get; set; }

        public string SalaryMonth { get; set; } = string.Empty;

        public Employee? Employee { get; set; }
    }
}