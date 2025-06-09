namespace WordMix.Api.Controllers;

using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Byndyusoft.ModelResult.AspNetCore.Extensions;
using Contracts;
using Domain.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize]
[ApiVersion("1.0")]
public class GameController
{
    /// <summary>
    ///     Начать игру
    /// </summary>
    [HttpPost("start")]
    public async Task<GameDto> StartGameAsync([FromServices] StartGameUseCase useCase, 
                                              CancellationToken cancellationToken)
    {
        return await useCase.Execute(cancellationToken);
    }

    /// <summary>
    ///     Проверить ответ на вопрос
    /// </summary>
    [HttpGet("check-answer")]
    public async Task<ActionResult<CheckAnswerResponseDto>> CheckAnswerAsync([FromQuery] long gameId,
                                                         [FromQuery] long answerId,
                                                         [FromServices] CheckAnswerUseCase useCase,
                                                         CancellationToken cancellationToken)
    {
        var res = await useCase.Ask(gameId, answerId, cancellationToken);

        return res.ToActionResult();
    }
}