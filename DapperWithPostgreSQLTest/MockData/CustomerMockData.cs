using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperWithPostgreSQL.Models;

namespace DapperWithPostgreSQLTest.MockData
{
    internal class CustomerMockData
    {
        public static List<Customer> GetCustomersMockData()
        {
            return new List<Customer> 
            {
                new Customer { Id=1,first_name="sudhar",last_name="sanan",email="sudhar@example.com",gender_id=1},
                new Customer { Id=2,first_name="arun",last_name="kumar",email="arun@example.com",gender_id=1},
                new Customer { Id=3,first_name="priya",last_name="rajesh",email="priya@example.com",gender_id=2}
            };
        }

        public static List<Customer> GetCustomersEmptyData()
        {
            return new List<Customer>();
        }
        public static Customer GetCustomersById()
        {
            return new Customer { Id = 1, first_name = "sudhar", last_name = "sanan", email = "sudhar@example.com", gender_id = 1 };

        }
        public static string GetCustomersByIdEmptyData()
        {
            return string.Empty;
        }


    }

    
}
