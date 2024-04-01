using Flunt.Notifications;
using Flunt.Validations;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Create;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public class AuthenticateSpecification
{
    public static Contract<Notification> Ensure(AuthenticateRequest authenticateRequest)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(authenticateRequest.Password.Length, 40, "Password", "A senha deve ser menor que 40 caracteres.")
            .IsGreaterThan(authenticateRequest.Password.Length, 8, "Password", "A senha deve ser maior que 8 caracteres.")
            .IsEmail(authenticateRequest.Email, "Email", "Email inválido.");
}