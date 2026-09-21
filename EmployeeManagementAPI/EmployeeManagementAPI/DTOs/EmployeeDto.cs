namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

       

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

       


        public DateTime JoiningDate { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string? Address { get; set; }

        public string Status { get; set; } = "Active";
    }

    public class CreateEmployeeDto
    {
     

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

       

      

        public DateTime JoiningDate { get; set; }

        public int DepartmentId { get; set; }

        public string Designation { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string? Address { get; set; }

        public string Status { get; set; } = "Active";
    }
}
