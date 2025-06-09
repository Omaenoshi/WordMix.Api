namespace WordMix.Api.Contracts;

/// <summary>
///     Дто ответа 
/// </summary>
public class AnswerOptionDto
{
    /// <summary>
    ///     Значение
    /// </summary>
    public string Value { get; set; } = null!;
    
    /// <summary>
    ///     Правильный ли ответ
    /// </summary>
    public bool IsCorrect { get; set; }
}