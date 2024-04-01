using Texugo.Login.Core.Contexts.AccountContext.Entities;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface ICreateService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}