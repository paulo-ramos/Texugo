using Texugo.Login.Core.Contexts.AccountContext.Entities;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IAuthenticateRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}