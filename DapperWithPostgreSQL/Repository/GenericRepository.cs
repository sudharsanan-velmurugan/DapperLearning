﻿
using Dapper;
using Npgsql;

namespace DapperWithPostgreSQL.Repository
{
    public class GenericRepository<T> :IGenericRepository<T> where T : class
    {
        private readonly IConfiguration _config;

        public GenericRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task AddAsync(T entity)
        {

            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            var query = $"SELECT * FROM {tableName} WHERE \"Id\"={id}";
            await connection.QueryFirstOrDefaultAsync<T>(query);
           
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            string query = $"SELECT * FROM {tableName}";
            var results = await connection.QueryAsync<T>(query); 
            return results.ToList();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            string tableName = typeof(T).Name.ToLower();
            var query = $"SELECT * FROM {tableName} WHERE \"Id\"={id}";
            var result = await connection.QueryFirstOrDefaultAsync<T>(query);
            return result;
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
        private NpgsqlConnection GetConnection()
        {
            var connection = _config.GetConnectionString("postgre");

            return new NpgsqlConnection(connection);
        }
    }
}