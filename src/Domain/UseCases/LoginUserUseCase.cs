namespace WordMix.Domain.UseCases;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Byndyusoft.ModelResult.Common;
using Byndyusoft.ModelResult.ModelResults;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Options;
using Repositories;

public class LoginUserUseCase(IDbSessionFactory sessionFactory,
                              IUsersRepository usersRepository,
                              IOptions<JwtSettings> jwtOptionsAccessor,
                              IPlayersRepository playersRepository)
{
    public async Task<ModelResult<TokenDto>> ExecuteAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateSessionAsync(cancellationToken);
        var user = await usersRepository.GetByEmailAsync(dto.Email, cancellationToken);
        if (user is null)
            return CommonErrorResult.NotFound;
        
        var player = await playersRepository.GetByUserIdAsync(user.Id, cancellationToken);

        if (!VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            return new ErrorModelResult("Password", "Неверный пароль");

        var jwtOptions = jwtOptionsAccessor.Value;
        var token = GenerateJwtToken(player!, jwtOptions);
        
        return new TokenDto
                   {
                       AccessToken = token
                   };
    }

    private static bool VerifyPassword(string password, byte[] passwordHash, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var combined = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, combined, passwordBytes.Length, salt.Length);
        var computedHash = SHA256.HashData(combined);

        return computedHash.SequenceEqual(passwordHash);
    }

    private static string GenerateJwtToken(Player player, JwtSettings options)
    {
        var claims = new[]
                         {
                             new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                         };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                                         issuer: options.Issuer,
                                         audience: options.Audience,
                                         claims: claims,
                                         expires: DateTime.UtcNow.Add(options.TokenLifetime),
                                         signingCredentials: creds
                                        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}