using Flunt.Notifications;
using Flunt.Validations;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Create;

public static class CreateSpecification
{
    public static Contract<Notification> Ensure(CreateRequest createRequest)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(createRequest.Name.Length, 120, "Name", "O nome deve ser menor que 120 caracteres.")
            .IsGreaterThan(createRequest.Name.Length, 3, "Name", "O nome deve ser maior que 3 caracteres.")
            .IsLowerThan(createRequest.Password.Length, 40, "Password", "A senha deve ser menor que 40 caracteres.")
            .IsGreaterThan(createRequest.Password.Length, 8, "Password", "A senha deve ser maior que 8 caracteres.")
            .IsEmail(createRequest.Email, "Email", "Email inválido.");
}
