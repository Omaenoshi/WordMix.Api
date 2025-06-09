namespace WordMix.Domain.Entities;

/// <summary>
///     Слово для игры
/// </summary>
public class Word
{
    /// <summary>
    ///     ИД
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    ///     Слово
    /// </summary>
    public string Value { get; init; } = null!;
}