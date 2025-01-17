
using Dapper;
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
            string tableName = typeof(T).Name.ToLower();
            string query = GenericHelper.GetAllQuery(tableName);
            var results = await connection.QueryAsync<T>(query);
            return results.ToList();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            string query = GenericHelper.GetByIdQuery(id, tableName);
            var result = await connection.QueryFirstOrDefaultAsync<T>(query);
            return result;
        }

        public async Task AddAsync(T entity)
        {

            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            string query = GenericHelper.GetInsertQuerry(tableName);
            if (query != null)
            {
                await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            string query = GenericHelper.GetUpdateQuerry(tableName);
            if (query != null)
            {
                await connection.ExecuteAsync(query,entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            string deleteQuery = $"DELETE FROM {tableName} WHERE \"Id\"={id}";
            await connection.ExecuteAsync(deleteQuery);

        }
        private NpgsqlConnection GetConnection()
        {
            var connection = _config.GetConnectionString("postgre");

            return new NpgsqlConnection(connection);
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
