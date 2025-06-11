namespace WordMix.Domain.Entities;

using Byndyusoft.Data.Relational.QueryBuilder.Abstractions.Extensions;

/// <summary>
///     Игра
/// </summary>
public class Game : IEntity
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