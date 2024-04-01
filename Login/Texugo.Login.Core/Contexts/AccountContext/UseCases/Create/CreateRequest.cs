namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Create;
using MediatR;

public record CreateRequest(
    string Name, 
    string Email,
    string Password
) : IRequest<CreateResponse>;

