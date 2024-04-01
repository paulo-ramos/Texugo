using System.Text.RegularExpressions;
using Texugo.Login.Core.Contexts.SharedContext.Extensions;
using Texugo.Login.Core.Contexts.SharedContext.ValueObjects;

namespace Texugo.Login.Core.Contexts.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected Email()
    {
    }

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("Email inválido");
        
        Address = address.Trim().ToLower();
        
        if (Address.Length < 5)
            throw new Exception("Email inválido");
        
        if (!EmailRegex().IsMatch(Address))
            throw new Exception("Email inválido");
    }
    public string Address { get; private set; }
    public string Hash => Address.ToBase64(); //Gravatar.com
    public Verification Verification { get; private set; } = new();

    public void ResendVerification() => Verification = new Verification();

    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string email) => new Email(email);
    public override string ToString() => Address.Trim().ToLower();

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
    
}