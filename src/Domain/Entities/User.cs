namespace WordMix.Domain.Entities;

using System;

/// <summary>
///     Пользователь
/// </summary>
public class User 
{
    /// <summary>
    ///     ИД
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    ///     Электронная почта
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    ///     Хеш пароля
    /// </summary>
    public byte[] PasswordHash { get; set; } = null!;
    
    /// <summary>
    ///     Соль пароля
    /// </summary>
    public byte[] PasswordSalt { get; set; } = null!;
    
    /// <summary>
    ///     Подтверждена ли учетная запись
    /// </summary>
    public bool IsVerified { get; set; }
    
    /// <summary>
    ///     Токен для подтверждения
    /// </summary>
    public string VerificationToken { get; set; } = null!;
    
    /// <summary>
    ///     Время создания
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    ///     Время обновления
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
}