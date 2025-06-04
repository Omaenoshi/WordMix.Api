namespace WordMix.Domain.UseCases;

using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Repositories;

public class ConfirmEmailUseCase(IDbSessionFactory sessionFactory, IUserRepository userRepository)
{
    public async Task ExecuteAsync(string verificationToken, CancellationToken cancellationToken)
    {
        await using var session = await sessionFactory.CreateCommittableSessionAsync(cancellationToken);
        
        var user = await userRepository.GetByVerificationTokenAsync(verificationToken, cancellationToken);
        
        if (user == null)
            return;
        
        user.IsVerified = true;
        await userRepository.UpdateAsync(user, cancellationToken);
        
        await session.CommitAsync(cancellationToken);
    }
}