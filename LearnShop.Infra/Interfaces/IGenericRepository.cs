namespace LearnShop.Infra.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(long id);
    Task<T?> InsertAsync(T entity);
    Task<T?> UpdateAsync(long id, T entity);
    Task DeleteAsync(long id);
}