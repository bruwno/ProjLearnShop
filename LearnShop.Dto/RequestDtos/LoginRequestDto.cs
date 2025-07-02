namespace LearnShop.Dto.RequestDtos;

public record LoginRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}