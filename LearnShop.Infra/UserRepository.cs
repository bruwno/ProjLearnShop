using System.Data;
using Dapper;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Users;

namespace LearnShop.Infra;

public class UserRepository : BaseRepository, IUserRepository
{ 
    public UserRepository(IDbConnection dbConnection) : base(dbConnection)
    {
    }

    public async Task<IEnumerable<User?>> GetAllAsync()
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
            SELECT id, 
                   full_name AS FullName, 
                   email, 
                   password_hash AS PasswordHash, 
                   cpf, 
                   role 
            FROM Users";
            return await connection.QueryAsync<User>(sql);
        });
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
            SELECT id, 
                   full_name AS FullName, 
                   email, 
                   password_hash AS PasswordHash, 
                   cpf, 
                   role 
            FROM Users 
            WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        });
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
            SELECT id, 
                   full_name AS FullName, 
                   email, 
                   password_hash AS PasswordHash, 
                   cpf, 
                   role 
            FROM Users 
            WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        });
    }
    
    public async Task<User?> InsertAsync(User user)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
                INSERT INTO Users (full_name, email, password_hash, cpf, role)
                VALUES (@FullName, @Email, @PasswordHash, @Cpf, @Role)
                RETURNING id, full_name AS FullName, email, password_hash AS PasswordHash, cpf, role;
                ";

            var parameters = new
            {
                user.FullName,
                user.Email,
                user.PasswordHash,
                user.Cpf,
                Role = user.Role.ToString().ToLower()
            };

            var newUser = await connection.QuerySingleOrDefaultAsync<User>(sql, parameters);
            return newUser;
        });
    }

    public async Task<User?> UpdateAsync(long id, User user)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
                UPDATE Users 
                SET full_name = @FullName, 
                    email = @Email, 
                    password_hash = @PasswordHash, 
                    cpf = @Cpf, 
                    role = @Role
                WHERE id = @Id
                RETURNING id, full_name AS FullName, cpf, password_hash AS PasswordHash, email, role;
            ";

            var parameters = new
            {
                Id = id,
                user.FullName,
                user.Cpf,
                user.Email,
                user.PasswordHash,
                Role = user.Role.ToString().ToLower()
            };

            var updatedUser = await connection.QuerySingleOrDefaultAsync<User>(sql, parameters);
            return updatedUser;
        });
    }

    public async Task DeleteAsync(long id)
    {
        await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "DELETE FROM Users WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        });
    }
}