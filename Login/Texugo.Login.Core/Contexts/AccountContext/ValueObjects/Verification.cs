using Texugo.Login.Core.Contexts.SharedContext.ValueObjects;

namespace Texugo.Login.Core.Contexts.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public Verification()
    {
    }
    
    public string Code { get; private set; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;

    public void Verify(string code)
    {
        if (IsActive)
        {
            throw new Exception("Este token já foi ativado.");
        }

        if (ExpiresAt < DateTime.UtcNow)
        {
            throw new Exception("Este token já expirou.");
        }

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Token de verificação inválido.");
        }

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}