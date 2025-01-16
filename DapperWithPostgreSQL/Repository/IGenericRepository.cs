namespace DapperWithPostgreSQL.Repository
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T>GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
