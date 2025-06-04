namespace WordMix.Domain.UseCases;

using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Byndyusoft.ModelResult.Common;
using Byndyusoft.ModelResult.ModelResults;
using Repositories;
using Services.Interfaces;

public class GetPlayerUseCase(IDbSessionFactory dbSessionFactory, 
                              IPlayerRepository playerRepository, 
                              IUserRepository userRepository,
                              ICurrentUserService currentUserService)
{
    public async Task<ModelResult<PlayerDto>> GetAsync(CancellationToken cancellationToken)
    {
        await using var session = await dbSessionFactory.CreateSessionAsync(cancellationToken);
        
        var id = currentUserService.UserId;
        var player = await playerRepository.GetAsync(id, cancellationToken);

        if (player == null)
            return CommonErrorResult.NotFound;
        
        var user = await userRepository.GetByIdAsync(player.Id, cancellationToken);

        return new PlayerDto
                   {
                        Id = player.Id,
                        Username = player.Username,
                        Email = user!.Email,
                        Balance = player.Balance,
                        Experience = player.Experience,
                        Level = CalculateLevel(player.Experience)
                   };
    }
    
    private static int CalculateLevel(int experience)
    {
        var level = 1;

        while (true)
        {
            var requiredXp = GetXpForLevel(level + 1);
            if (experience < requiredXp)
                return level;
            level++;
        }

        static int GetXpForLevel(int level)
        {
            const int a = 50;
            const int b = 50;
            return a * level * level + b * level;
        }
    }
}