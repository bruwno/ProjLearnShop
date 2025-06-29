using LearnShop.Model.Enums;

namespace LearnShop.Dto.RequestDtos;

public class UserUpdateRequestDto
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Cpf { get; set; }
    public Role? Role { get; set; }
}