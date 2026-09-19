namespace EmployeeManagementAPI.DTOs
{
    public class PerformanceDto
    {
            public int PerformanceId { get; set; }

            public int EmployeeId { get; set; }

            public DateTime ReviewDate { get; set; }

            public int Rating { get; set; }

            public string Comments { get; set; } = string.Empty;

            public string Reviewer { get; set; } = string.Empty;
        
    }


    public class CreatePerformanceDto
    {
        public int EmployeeId { get; set; }

        public DateTime ReviewDate { get; set; }

        public int Rating { get; set; }

        public string? Comments { get; set; }
    }
}