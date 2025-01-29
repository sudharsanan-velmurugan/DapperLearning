using DapperLearning.Attribute;

namespace DapperLearning.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string EmployeeName { get; set; }

        [DesignationValidation]
        public  string Designation { get; set; }
        public required DateTime JoinedDate { get; set; }
    }
}
