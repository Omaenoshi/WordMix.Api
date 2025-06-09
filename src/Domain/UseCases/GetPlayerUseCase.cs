namespace WordMix.Domain.UseCases;

using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Byndyusoft.ModelResult.Common;
using Byndyusoft.ModelResult.ModelResults;
using Repositories;
using Services;
using Services.Interfaces;

public class GetPlayerUseCase(IDbSessionFactory dbSessionFactory, 
                              IPlayersRepository playersRepository, 
                              IUsersRepository usersRepository,
                              ICurrentPlayerService currentPlayerService)
{
    public async Task<ModelResult<PlayerDto>> GetAsync(CancellationToken cancellationToken)
    {
        await using var session = await dbSessionFactory.CreateSessionAsync(cancellationToken);
        
        var id = currentPlayerService.PlayerId;
        var player = await playersRepository.GetByIdAsync(id, cancellationToken);

        if (player == null)
            return CommonErrorResult.NotFound;
        
        var user = await usersRepository.GetByIdAsync(player.Id, cancellationToken);

        return new PlayerDto
                   {
                        Id = player.Id,
                        Username = player.Username,
                        Email = user!.Email,
                        Balance = player.Balance,
                        Experience = player.Experience,
                        Level = LevelService.CalculateLevel(player.Experience)
                   };
    }
}