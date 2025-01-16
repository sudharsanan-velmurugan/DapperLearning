using Dapper;
using DapperLearning.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DapperLearning.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration configuration;

        
        public EmployeeRepository( IConfiguration configuration )
        {
            this.configuration = configuration;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("INSERT INTO Employee(employeeName,designation,joinedDate)  VALUES(@employeeName,@designation,@joinedDate)",employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("DELETE FROM Employee WHERE Id=@ID",new {ID=id});
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            using var connection = GetConnection();

            var employees = await connection.QueryAsync<Employee>("SELECT * FROM Employee");

            return employees.ToList();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            var employee = await connection.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WHERE Id =@ID",new {ID=id});
            return employee;
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("UPDATE Employee SET employeeName=@employeeName,designation=@designation,joinedDate=@joinedDate WHERE Id =@Id", employee);
        }

        private SqlConnection GetConnection()
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connection);
        }
    }
}
