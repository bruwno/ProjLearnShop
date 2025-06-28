namespace LearnShop.Infra.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> InsertAsync(T entity);
    Task<T?> UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
}