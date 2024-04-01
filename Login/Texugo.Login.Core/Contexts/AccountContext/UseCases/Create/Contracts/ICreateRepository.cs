using Texugo.Login.Core.Contexts.AccountContext.Entities;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface ICreateRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
    
}