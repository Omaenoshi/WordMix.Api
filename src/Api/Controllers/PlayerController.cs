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
[Route("[controller]")]
[ApiVersion("1.0")]
public class PlayerController
{
    /// <summary>
    ///     Получить сведения о пользователе
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PlayerDto>> GetAsync([FromServices] GetPlayerUseCase useCase,
                                                        CancellationToken cancellationToken)
    {
        var res = await useCase.GetAsync(cancellationToken);

        return res.ToActionResult();
    }
}