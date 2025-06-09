namespace WordMix.Domain.UseCases;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Contracts;
using Byndyusoft.Data.Relational;
using Entities;
using Repositories;
using Services.Interfaces;

public class StartGameUseCase(IDbSessionFactory sessionFactory,
                              IGamesRepository gamesRepository,
                              IWordsRepository wordsRepository,
                              ICurrentPlayerService currentPlayerService)
{
    public async Task<GameDto> Execute(CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateCommittableSessionAsync(cancellationToken);
        
        var words = await wordsRepository.GetRandomWordsAsync(5, cancellationToken);

        var correctWord = words.First();
        var shuffledWord = ShuffleWord(correctWord.Value);

        var game = new Game
                       {
                           PlayerId = currentPlayerService.PlayerId,
                           CorrectWordId = correctWord.Id
                       };
        await gamesRepository.InsertAsync(game, cancellationToken);
        
        await session.CommitAsync(cancellationToken);

        return new GameDto
                   {
                       Id = game.Id,
                       Words = words.Select(ToWordDto).ToArray(),
                       ShuffledWord = shuffledWord
                   };
    }
    
    private static string ShuffleWord(string word)
    {
        var rng = new Random();
        return new string(word.ToCharArray().OrderBy(_ => rng.Next()).ToArray());
    }

    private static WordDto ToWordDto(Word word)
    {
        return new WordDto
                   {
                       Id = word.Id,
                       Value = word.Value,
                   };
    }
}