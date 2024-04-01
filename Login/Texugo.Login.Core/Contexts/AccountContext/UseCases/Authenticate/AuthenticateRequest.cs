using MediatR;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public record AuthenticateRequest(
    string Email, 
    string Password
) : IRequest<AuthenticateResponse>;
