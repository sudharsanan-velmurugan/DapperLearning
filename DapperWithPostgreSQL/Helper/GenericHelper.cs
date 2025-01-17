namespace DapperWithPostgreSQL.Helper
{
    public class GenericHelper
    {
        public static string GetInsertQuerry(string tableName)
        {
            switch (tableName)
            {
                case "customer":
                    return GetCustomerInsertQuery();
                case "gender":
                    return GetGenderInsertQuery();
                    default:
                    return null;
            }
        } 

        static string GetCustomerInsertQuery()
        {
            return "INSERT INTO customer(first_name,last_name,email,gender_id) VALUES(@first_name,@last_name,@email,@gender_id)";
        }
        static string GetGenderInsertQuery()
        {
            return "INSERT INTO gender(gender_name) VALUES(@gender_name)";
        }

        public static string GetUpdateQuerry(string tableName)
        {
            switch (tableName)
            {
                case "customer":
                    return GetCustomerUpdateQuery();
                case "gender":
                    return GetGenderUpdateQuery();
                default:
                    return null;
            }
        }

        static string GetCustomerUpdateQuery()
        {
            return "UPDATE customer SET first_name=@first_name,last_name=@last_name,email=@email,gender_id=@gender_id WHERE \"Id\"=@Id )";
        }
        static string GetGenderUpdateQuery()
        {
            return "UPDATE gender SET gender_name=@gander_name WHERE \"Id\"=@Id";
        }

    }
}
