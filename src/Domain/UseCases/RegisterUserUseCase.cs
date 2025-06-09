namespace WordMix.Domain.UseCases;

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Byndyusoft.ModelResult.ModelResults;
using Entities;
using Repositories;
using Services.Interfaces;

public class RegisterUserUseCase(
    IDbSessionFactory sessionFactory,
    IUsersRepository usersRepository,
    IPlayersRepository playersRepository,
    IEmailService emailService)
{
    public async Task<ModelResult> ExecuteAsync(RegisterUserDto dto, CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateCommittableSessionAsync(cancellationToken);

        var user = await usersRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (user != null)
            return new ErrorModelResult("Email", "Пользователь с такой почтой уже существует");

        var salt = GenerateSalt();
        var verificationToken = Guid.NewGuid().ToString("N");
        var newUser = new User
                          {
                              Email = dto.Email,
                              CreatedAt = DateTimeOffset.UtcNow,
                              IsVerified = false,
                              UpdatedAt = null,
                              PasswordHash = HashPassword(dto.Password, salt),
                              PasswordSalt = salt,
                              VerificationToken = verificationToken
                          };

        await usersRepository.InsertAsync(newUser, cancellationToken);

        var player = new Player
                         {
                             Username = dto.Username,
                             Balance = 0,
                             Experience = 0,
                             AvatarUrl = null,
                             UserId = newUser.Id
                         };

        await playersRepository.InsertAsync(player, cancellationToken);

        await session.CommitAsync(cancellationToken);

        var confirmationLink = $"https://your-app.com/confirm-email?token={verificationToken}";
        const string subject = "Подтверждение регистрации";
        var body = $"Пожалуйста, подтвердите вашу почту, перейдя по ссылке: {confirmationLink}";

        await emailService.SendAsync(dto.Email, subject, body, cancellationToken);

        return ModelResult.Ok;
    }

    private static byte[] GenerateSalt()
    {
        var buffer = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(buffer);
        return buffer;
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var combined = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, combined, passwordBytes.Length, salt.Length);
        return SHA256.HashData(combined);
    }
}