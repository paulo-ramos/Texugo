using Texugo.Login.Core.Contexts.AccountContext.ValueObjects;
using Texugo.Login.Core.Contexts.SharedContext.Entities;

namespace Texugo.Login.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    protected User()
    {
    }
    
    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    
    public User(string email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }
    public string Name { get; private set; } = String.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public String Image { get; private set; } = String.Empty;

    public List<Role> Roles { get; set; } = new();


    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Código informado não confere.");
        }

        var password = new Password(plainTextPassword);
        Password = password;
    }


    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
    
}