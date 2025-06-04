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
using Byndyusoft.ModelResult.Common;
using Byndyusoft.ModelResult.ModelResults;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Options;
using Repositories;

public class LoginUserUseCase(IUserRepository userRepository,
                              IOptions<JwtSettings> jwtOptionsAccessor)
{
    public async Task<ModelResult<TokenDto>> ExecuteAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);
        if (user is null)
            return CommonErrorResult.NotFound;

        if (!VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            return new ErrorModelResult("Password", "Неверный пароль");

        var jwtOptions = jwtOptionsAccessor.Value;
        var token = GenerateJwtToken(user, jwtOptions);
        
        return new TokenDto
                   {
                       AccessToken = token
                   };
    }

    private static bool VerifyPassword(string password, byte[] passwordHash, byte[] salt)
    {
        var combined = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
        var computedHash = SHA256.HashData(combined);

        return computedHash.SequenceEqual(passwordHash);
    }

    private static string GenerateJwtToken(User user, JwtSettings options)
    {
        var claims = new[]
                         {
                             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                             new Claim(JwtRegisteredClaimNames.Email, user.Email),
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