namespace WordMix.Api.Controllers;

using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Byndyusoft.ModelResult.AspNetCore.Extensions;
using Contracts;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Контроллер для работы с пользователями
/// </summary>
[ApiController]
[ApiVersion("1.0")]
public class UserController
{
    /// <summary>
    ///     Зарегистрировать пользователя
    /// </summary>
    [HttpPost("[action]")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto dto,
                                             [FromServices] RegisterUserUseCase useCase,
                                             CancellationToken cancellationToken)
    {
        var res = await useCase.ExecuteAsync(dto, cancellationToken);

        return res.ToActionResult();
    }
    
    /// <summary>
    ///     Подтверждение регистрации
    /// </summary>
    [HttpGet("confirm-email")]
    public async Task ConfirmEmail([FromQuery] string token,
                                                 [FromServices] ConfirmEmailUseCase useCase,
                                                 CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(token, cancellationToken);
    }
}