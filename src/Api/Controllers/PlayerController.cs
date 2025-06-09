namespace WordMix.Api.Controllers;

using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Byndyusoft.ModelResult.AspNetCore.Extensions;
using Byndyusoft.ModelResult.ModelResults;
using Contracts;
using Domain.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Контроллер для работы с игроком
/// </summary>
[ApiController]
[Route("players")]
[ApiVersion("1.0")]
[Authorize]
public class PlayerController
{
    /// <summary>
    ///     Получить сведения о пользователе
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PlayerDto>> GetAsync([FromServices] GetPlayerUseCase useCase,
                                                        CancellationToken cancellationToken)
    {
        var res = await useCase.GetAsync(cancellationToken);

        return res.ToActionResult();
    }

    /// <summary>
    ///     Получить лучших 10 пользователей
    /// </summary>
    [HttpGet("statistics")]
    public async Task<PlayerStatisticsDto> GetStatisticsAsync([FromServices] GetPlayerStatisticsUseCase useCase,
                                                              CancellationToken cancellationToken)
    {
        return await useCase.GetAsync(cancellationToken);
    }
}