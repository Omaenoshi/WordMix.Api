namespace WordMix.Domain.Entities;

using Byndyusoft.Data.Relational.QueryBuilder.Abstractions.Extensions;

/// <summary>
///     Игрок
/// </summary>
public class Player : IEntity
{
    /// <summary>
    ///     ИД
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    ///     Ник
    /// </summary>
    public string Username { get; set; } = null!;
    
    /// <summary>
    ///     Опыт
    /// </summary>
    public int Experience { get; set; }
    
    /// <summary>
    ///     Баланс
    /// </summary>
    public int Balance { get; set; }
    
    /// <summary>
    ///     Ссылка на аватар
    /// </summary>
    public string? AvatarUrl { get; set; }
    
    /// <summary>
    ///     ИД пользователя
    /// </summary>
    public long UserId { get; set; }
}