namespace WordMix.Domain.Services;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Http;

public class CurrentUserService : ICurrentUserService
{
    public long UserId { get; }

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("Пользователь не авторизован");

        var claim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst(JwtRegisteredClaimNames.Sub);
        if (claim is null || !long.TryParse(claim.Value, out var id))
            throw new UnauthorizedAccessException("Не удалось извлечь ID пользователя из JWT");

        UserId = id;
    }
}