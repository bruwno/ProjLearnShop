using LearnShop.Model.Enums;

namespace LearnShop.Model.Users;

public class User : BaseModel
{
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; }

    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password), "A senha não pode ser nula ou vazia.");
        }
        
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool CheckPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}