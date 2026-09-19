namespace EmployeeManagementAPI.DTOs
{
    public class SalaryDto
    {
            public int SalaryId { get; set; }

            public int EmployeeId { get; set; }

            public decimal BasicSalary { get; set; }

            public decimal Allowance { get; set; }

            public decimal Deduction { get; set; }

            public decimal NetSalary { get; set; }

            public string SalaryMonth { get; set; } = string.Empty;
        
    }


    public class CreateSalaryDto
    {
        public int EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public int SalaryMonth { get; set; }

        public int SalaryYear { get; set; }
    }
}