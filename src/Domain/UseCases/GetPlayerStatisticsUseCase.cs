namespace WordMix.Domain.UseCases;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Entities;
using Repositories;
using Services;

public class GetPlayerStatisticsUseCase(IDbSessionFactory sessionFactory, IPlayersRepository playersRepository)
{
    public async Task<PlayerStatisticsDto> GetAsync(CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateSessionAsync(cancellationToken);

        var players = await playersRepository.GetOrderByScore(10, cancellationToken);

        return new PlayerStatisticsDto
                   {
                       PlayerStatistics = players.Select(ToPlayerStatisticDto).ToArray()
                   };
    }

    private static PlayerStatisticDto ToPlayerStatisticDto(Player player)
    {
        return new PlayerStatisticDto
                   {
                       Username = player.Username,
                       Level = LevelService.CalculateLevel(player.Experience)
                   };
    }
}