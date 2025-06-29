using LearnShop.Model.Enums;

namespace LearnShop.Dto.RequestDtos;

public record UserCreateRequestDto
{
    public required string FullName { get; set; } = string.Empty;
    public required string Cpf { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required Role Role { get; set; }
    
}