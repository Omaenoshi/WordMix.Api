namespace WordMix.Domain.UseCases;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Entities;
using Repositories;
using Services;
using Services.Interfaces;

public class GetPlayerStatisticsUseCase(IDbSessionFactory sessionFactory, IPlayersRepository playersRepository, ICurrentPlayerService currentPlayerService)
{
    public async Task<PlayerStatisticsDto> GetAsync(CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateSessionAsync(cancellationToken);

        var player = await playersRepository.GetByIdAsync(currentPlayerService.PlayerId, cancellationToken);

        return new PlayerStatisticsDto
                   {
                       Date = DateTimeOffset.Now,
                       Score = player!.Experience
                   };
    }
}