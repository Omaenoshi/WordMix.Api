namespace WordMix.Api.Contracts;

/// <summary>
///     Дто слова
/// </summary>
public class WordDto
{
    /// <summary>
    ///     ИД
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    ///     Слово
    /// </summary>
    public string Value { get; set; } = default!;
}