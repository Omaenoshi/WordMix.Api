namespace WordMix.Domain.Entities;

using Byndyusoft.Data.Relational.QueryBuilder.Abstractions.Extensions;

/// <summary>
///     Слово для игры
/// </summary>
public class Word : IEntity
{
    /// <summary>
    ///     ИД
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    ///     Слово
    /// </summary>
    public string Value { get; init; } = null!;
    
    public string? Img {get; set;}
}