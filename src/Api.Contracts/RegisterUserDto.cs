namespace WordMix.Api.Contracts;

using System.ComponentModel.DataAnnotations;
using Byndyusoft.MaskedSerialization.Annotations.Attributes;

/// <summary>
///     Дто для регистрации пользователя
/// </summary>
public class RegisterUserDto
{
    /// <summary>
    ///     Электронная почта
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    ///     Никнейм 
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;

    /// <summary>
    ///     Пароль
    /// </summary>
    [Masked]
    public string Password { get; set; } = null!;
}