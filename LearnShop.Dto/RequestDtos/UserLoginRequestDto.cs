namespace LearnShop.Dto.RequestDtos;

public record UserLoginRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}