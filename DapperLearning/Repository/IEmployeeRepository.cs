using DapperLearning.Models;

namespace DapperLearning.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(int id);

        Task AddEmployeeAsync(Employee employee);

        Task UpdateEmployeeAsync(int id,Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
