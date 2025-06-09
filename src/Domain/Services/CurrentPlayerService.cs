namespace WordMix.Domain.Services;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Http;

public class CurrentPlayerService : ICurrentPlayerService
{
    public long PlayerId { get; }

    public CurrentPlayerService(IHttpContextAccessor accessor)
    {
        var player = accessor.HttpContext?.User;

        if (player?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("Пользователь не авторизован");

        var claim = player.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null || long.TryParse(claim.Value, out var id) == false)
            throw new UnauthorizedAccessException("Не удалось извлечь ID пользователя из JWT");

        PlayerId = id;
    }
}