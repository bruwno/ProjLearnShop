using System.Data;
using Dapper;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Products;

namespace LearnShop.Infra;

public class EbookRepository : BaseRepository, IEbookRepository
{
    public EbookRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Ebook?>> GetAllAsync()
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "SELECT id, title, description, image_url AS ImageUrl, author, publisher, category, price " +
                               "FROM ebooks";
            return await connection.QueryAsync<Ebook>(sql);
        });
    }

    public async Task<Ebook?> GetByIdAsync(long id)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "SELECT id, title, description, image_url AS ImageUrl, author, publisher, category, price " +
                               "FROM ebooks WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Ebook>(sql, new { Id = id });
        });
    }

    public async Task<Ebook?> InsertAsync(Ebook entity)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
                INSERT INTO ebooks(title, description, image_url, author, publisher, category, price)
                VALUES (@Title, @Description, @ImageUrl, @Author, @Publisher, @Category, @Price)
                RETURNING id, title, description, image_url AS ImageUrl, author, publisher, category, price";

            return await connection.QueryFirstOrDefaultAsync<Ebook>(sql, new
            {
                entity.Title,
                entity.Description,
                entity.ImageUrl,
                entity.Author,
                entity.Publisher,
                entity.Category,
                entity.Price
            });
        });
    }

    public async Task<Ebook?> UpdateAsync(long id, Ebook entity)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
                UPDATE ebooks
                SET title = @Title,
                    description = @Description,
                    image_url = @ImageUrl,
                    author = @Author,
                    publisher = @Publisher,
                    category = @Category,
                    price = @Price
                WHERE Id = @Id;
                SELECT * FROM ebooks WHERE Id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<Ebook>(sql, new
            {
                Id = id,
                entity.Title,
                entity.Description,
                entity.ImageUrl,
                entity.Author,
                entity.Publisher,
                entity.Category,
                entity.Price
            });
        });
    }

    public async Task DeleteAsync(long id)
    {
        await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "DELETE FROM ebooks WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        });
    }
}