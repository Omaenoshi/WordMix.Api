namespace WordMix.Api.Contracts;

using System.ComponentModel.DataAnnotations;
using Byndyusoft.MaskedSerialization.Annotations.Attributes;

/// <summary>
///     Дто логина
/// </summary>
public class LoginDto
{
    /// <summary>
    ///     Почта
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;
    
    /// <summary>
    ///     Пароль
    /// </summary>
    [Required]
    [Masked]
    public string Password { get; set; } = null!;
}