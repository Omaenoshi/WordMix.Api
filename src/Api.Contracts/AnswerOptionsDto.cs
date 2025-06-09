namespace WordMix.Api.Contracts;

/// <summary>
///     Дто вариантов ответа
/// </summary>
public class AnswerOptionsDto
{
    /// <summary>
    ///     Варианты ответа
    /// </summary>
    public AnswerOptionDto[] Options { get; set; } = null!;
}