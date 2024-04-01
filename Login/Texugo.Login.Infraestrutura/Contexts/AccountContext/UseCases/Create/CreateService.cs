using SendGrid;
using SendGrid.Helpers.Mail;
using Texugo.Login.Core;
using Texugo.Login.Core.Contexts.AccountContext.Entities;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace Texugo.Login.Infraestrutura.Contexts.AccountContext.UseCases.Create;

public class CreateService :ICreateService
{
    public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    {
        var apiKey = Configuration.SendGrid.ApiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        const string subject = "Verifique sua conta!";
        var to = new EmailAddress(user.Email, user.Name);
        var plainTextContent = $"Codigo de ativação: {user.Email.Verification.Code} \n Válido até: {user.Email.Verification.ExpiresAt}";
        var htmlContent = $"Codigo de ativação:<strong>{user.Email.Verification.Code}</strong><br>Válido até:<strong>{user.Email.Verification.ExpiresAt}</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg, cancellationToken).ConfigureAwait(false);
    }
}