using System.Data;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Products;

namespace LearnShop.Infra;

public class EbookRepository : BaseRepository, IEbookRepository
{
    public EbookRepository(IDbConnection connection) : base(connection)
    {
    }

    public Task<IEnumerable<Ebook?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Ebook?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Ebook?> InsertAsync(Ebook entity)
    {
        throw new NotImplementedException();
    }

    public Task<Ebook?> UpdateAsync(Guid id, Ebook entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}