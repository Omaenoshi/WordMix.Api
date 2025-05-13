namespace WordMix.Domain.Entities;

/// <summary>
///     Игрок
/// </summary>
public class Player
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
}