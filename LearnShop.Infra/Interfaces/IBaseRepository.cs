using LearnShop.Model;

namespace LearnShop.Infra.Interfaces;

public interface IBaseRepository<T> where T: IModel
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(long id);
    Task InsertAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(long id);
}