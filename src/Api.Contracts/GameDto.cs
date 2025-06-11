namespace WordMix.Api.Contracts;

/// <summary>
///     Дто игры
/// </summary>
public class GameDto
{
    /// <summary>
    ///     ИД игры
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Слова
    /// </summary>
    public WordDto[] Words { get; set; } = default!;
    
    /// <summary>
    ///     Вопрос
    /// </summary>
    public string Img { get; set; } = default!;
}