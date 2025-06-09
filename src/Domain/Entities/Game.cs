namespace WordMix.Domain.Entities;

using Api.Contracts;

/// <summary>
///     Игра
/// </summary>
public class Game
{
    /// <summary>
    ///     ИД
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    ///     ИД игрока
    /// </summary>
    public long PlayerId { get; set; }
    
    /// <summary>
    ///     ИД правильного слова
    /// </summary>
    public long CorrectWordId { get; set; }
}