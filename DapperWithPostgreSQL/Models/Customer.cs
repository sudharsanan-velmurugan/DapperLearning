using System.Net;
using System.Security.Cryptography.Xml;

namespace DapperWithPostgreSQL.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public required string first_name { get; set; }
        public required string last_name { get; set; }
        public required string email { get; set; }
        public int  gender_id { get; set; }  
      
    }
}
