using Dapper;
using DapperWithPostgreSQL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;

namespace DapperWithPostgreSQL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _config;

        public CustomerRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (customer != null)
            {
               using var connection = GetConnection();
                await connection.ExecuteAsync("INSERT INTO customer(first_name,last_name,email,gender_id) VALUES(@first_name,@last_name,@email,@gender_id)", customer);
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
           using  var connection = GetConnection();
            await connection.ExecuteAsync("DELETE FROM customer WHERE \"Id\"=@ID", new {ID=id});
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            using var connection = GetConnection();
            var customers = await connection.QueryAsync<Customer>("SELECT * FROM customer");
            return customers.ToList();
        }

        public async Task<Customer> GetByIdAsync(int Id)
        {
            var connection = GetConnection();
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM customer WHERE \"Id\"=@id", new {id=Id});
            return customer;
    
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var connection = GetConnection();
            await connection.ExecuteAsync("UPDATE  customer SET first_name=@first_name,last_name=@last_name,email=@email,gender_id=@gender_id WHERE \"Id\"=@Id ",customer);
        }

        private NpgsqlConnection GetConnection()
        {
            var connection = _config.GetConnectionString("postgre");

            return new NpgsqlConnection(connection);
        }
    }
}
