using System.Reflection;

namespace DapperWithPostgreSQL.Helper
{
    public class GenericHelper
    {
        public static string GetAllQuery(string tableName)
        { 
            return $"SELECT * FROM {tableName}"; ;
        }

        public static string GetByIdQuery(string tableName)
        {
            return $"SELECT * FROM {tableName} WHERE \"Id\"=@id";
        }

        public static string GetInsertQuery<T>(string tableName)
        {
            var prop = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var colums = string.Join(",", prop.Where(x=>x.Name!="Id").Select(x => x.Name));
            var values = string.Join(",", prop.Where(x => x.Name != "Id").Select(x => "@" + x.Name));
            string query = $"INSERT INTO {tableName}({colums}) VALUES ({values})";
            return query;

        }
        public static string GetUpdateQuery<T>(string tableName)
        {
            var prop = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var values = string.Join(",", prop.Where(x=>x.Name!="Id").Select(x => x.Name+"="+"@" + x.Name));

            var query = $"UPDATE {tableName} SET {values} WHERE \"Id\"=@id";
            return query;
            //switch (tableName)
            //{
            //    case "customer":
            //        return GetCustomerUpdateQuery();
            //    case "gender":
            //        return GetGenderUpdateQuery();
            //    default:
            //        return null;
            //}
        }
        public static string GetDeleteQuery(string tableName)
        {
            return $"DELETE FROM {tableName} WHERE \"Id\"=@id";
        }
        //static string GetCustomerInsertQuery()
        //{
        //    return "INSERT INTO customer(first_name,last_name,email,gender_id) VALUES(@first_name,@last_name,@email,@gender_id)";
        //}
        //static string GetGenderInsertQuery()
        //{
        //    return "INSERT INTO gender(gender_name) VALUES(@gender_name)";
        //}

        //static string GetCustomerUpdateQuery()
        //{
        //    return "UPDATE customer SET first_name=@first_name,last_name=@last_name,email=@email,gender_id=@gender_id WHERE \"Id\"=@Id";
        //}
        //static string GetGenderUpdateQuery()
        //{
        //    return "UPDATE gender SET gender_name=@gender_name WHERE \"Id\"=@Id";
        //}



    }
}
