﻿using LearnShop.Model.Enums;

namespace LearnShop.Dto.RequestDtos;

public record UserCreateRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; }
}