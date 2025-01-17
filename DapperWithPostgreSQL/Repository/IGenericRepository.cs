using DapperWithPostgreSQL.Models;

namespace DapperWithPostgreSQL.Repository
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<CustomerWithGender>> GetCustomersWithGenderAsync();
        Task<T>GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        
    }
}
