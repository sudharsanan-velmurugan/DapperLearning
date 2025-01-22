
using System.Reflection;
using Dapper;
using DapperWithPostgreSQL.Attributes;
using DapperWithPostgreSQL.Helper;
using DapperWithPostgreSQL.Models;
using Npgsql;

namespace DapperWithPostgreSQL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IConfiguration _config;

        public GenericRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var connection = GetConnection();
            string tableName = GetTableName();
            // string query = GenericHelper.GetAllQuery(tableName);
            var query = File.ReadAllText(Directory.GetCurrentDirectory()+"/SQLQueries/GetAllCustomerQuery.sql");
            var results = await connection.QueryAsync<T>(query);
            return results.ToList();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            string tableName = GetTableName();
     //       string query = GenericHelper.GetByIdQuery(tableName);
            var query = File.ReadAllText(Directory.GetCurrentDirectory() + "/SQLQueries/GetCustomerByIdQuery.sql");
            var result = await connection.QueryFirstOrDefaultAsync<T>(query, new {id});
            return result;
        }

        public async Task AddAsync(T entity)
        {

            using var connection = GetConnection();
            string tableName = GetTableName();
            //string query = GenericHelper.GetInsertQuery<T>(tableName);
            var query=File.ReadAllText(Directory.GetCurrentDirectory()+"/SQLQueries/AddCustomerQuery.sql");
            if (query != null)
            {
                await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using var connection = GetConnection();
            string tableName = GetTableName();
            //string query = GenericHelper.GetUpdateQuery<T>(tableName);
            var query = File.ReadAllText(Directory.GetCurrentDirectory() + "/SQLQueries/UpdateCustomerQuery.sql");

            if (query != null)
            {
                await connection.ExecuteAsync(query,entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            //string query = GenericHelper.GetDeleteQuery(tableName);
            var query = File.ReadAllText(Directory.GetCurrentDirectory() + "/SQLQueries/DeleteCustomerQuery.sql");

            await connection.ExecuteAsync(query, new {id});
        }
        private NpgsqlConnection GetConnection()
        {
            var connection = _config.GetConnectionString("postgre");

            return new NpgsqlConnection(connection);
        }
        private string GetTableName()
        {
            var attribute = typeof(T).GetCustomAttribute<TableAttribute>();
            return attribute?.Name ?? typeof(T)?.Name.ToLower();
        }
        public async Task<List<CustomerWithGender>> GetCustomersWithGenderAsync()
        {
            using var connection = GetConnection();
            string query = $"SELECT customer.\"Id\",first_name,last_name,email,gender_name AS gender FROM customer JOIN gender ON customer.gender_id = gender.\"Id\" ";
            var result = await connection.QueryAsync<CustomerWithGender>(query);
            return result.ToList();
        }
    }
}
