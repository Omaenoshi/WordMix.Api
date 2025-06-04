namespace WordMix.Api.Contracts;

/// <summary>
///     Дто игрока
/// </summary>
public class PlayerDto
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
    ///     Почта
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    ///     Баланс
    /// </summary>
    public int Balance { get; set; }
    
    /// <summary>
    ///     Опыт
    /// </summary>
    public int Experience { get; set; }
    
    /// <summary>
    ///     Уровень
    /// </summary>
    public int Level { get; set; }
}