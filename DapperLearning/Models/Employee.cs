namespace DapperLearning.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string EmployeeName { get; set; }

        public required string Designation { get; set; }
        public required DateTime JoinedDate { get; set; }
    }
}
