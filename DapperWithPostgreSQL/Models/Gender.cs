using DapperWithPostgreSQL.Attributes;

namespace DapperWithPostgreSQL.Models
{
    [Table("gender")]
    public class Gender
    {
        public int Id { get; set; }
        public required string gender_name { get; set; }

    }
}
