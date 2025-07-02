using LearnShop.Model.Enums;

namespace LearnShop.Model.Users;

public class UserInfo
{
    public long UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Username => GetFirstName();
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    
    private string GetFirstName()
    {
        return FullName.Split(' ').FirstOrDefault() ?? string.Empty;
    }
}