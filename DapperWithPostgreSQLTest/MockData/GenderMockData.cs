using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperWithPostgreSQL.Models;

namespace DapperWithPostgreSQL.Test.MockData
{
    public class GenderMockData
    {
        public static List<Gender> GetGenderMockData()
        {
            return new List<Gender>
            {
                new Gender { Id=1,gender_name="Male"},
                new Gender { Id=2,gender_name="Female" }
            };
        }

        public static List<Gender> GetGenderEmptyData()
        {
            return new List<Gender>();
        }
        public static Gender GetGenderById()
        {
            return new Gender { Id = 1, gender_name = "Male" };

        }
    }
}
