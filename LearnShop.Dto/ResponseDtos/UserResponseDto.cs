using LearnShop.Model.Enums;

namespace LearnShop.Dto.ResponseDtos;

public class UserResponseDto
{
    public long Id { get; set; }
    public required string? FullName { get; set; }
    public required string? Email { get; set; }
    public Role? Role { get; set; }
}