namespace WordMix.Domain.UseCases;

using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Byndyusoft.ModelResult.Common;
using Byndyusoft.ModelResult.ModelResults;
using Repositories;
using Services.Interfaces;

public class CheckAnswerUseCase(IDbSessionFactory sessionFactory, 
                                IGamesRepository gamesRepository, 
                                ICurrentPlayerService currentPlayerService,
                                IPlayersRepository playersRepository)
{
    public async Task<ModelResult<CheckAnswerResponseDto>> Ask(long gameId, long answerId, CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateSessionAsync(cancellationToken);
        
        var game = await gamesRepository.GetByIdAsync(gameId, cancellationToken);

        if (game == null)
            return CommonErrorResult.NotFound;

        var result = game.CorrectWordId == answerId;

        if (result == false)
            return new CheckAnswerResponseDto
                       {
                           Result = result
                       };
        
        var player = await playersRepository.GetByIdAsync(game.PlayerId, cancellationToken);
        player!.Experience += 100;
        await playersRepository.UpdateAsync(player, cancellationToken);

        return new CheckAnswerResponseDto
                   {
                       Result = result
                   };
    }
}