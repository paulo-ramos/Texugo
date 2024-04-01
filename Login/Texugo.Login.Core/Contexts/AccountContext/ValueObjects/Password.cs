using System.Security.Cryptography;
using Texugo.Login.Core.Contexts.SharedContext.ValueObjects;

namespace Texugo.Login.Core.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Special = "!@#$%^&*(){}[];";
    private const string number = "1234567890";
    private const string allChars = Valid + Special + number;


    protected Password()
    {
    }
    
    public Password(string? text = null)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = Generate();

        Hash = Hashing(text);
    }

    public bool Challenge(string plainTextPassword)
    {
        return Verify(Hash, plainTextPassword);
    }

    public string Hash { get; private set; } = String.Empty;
    public string ResetCode { get; private set; } = Guid.NewGuid().ToString("N")[..6].ToUpper();

    
    private static string Generate(short length = 16, bool includeSpecialChars = true, bool upperCase = false)
    {
        var chars = includeSpecialChars ? allChars : Valid;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var res = new char[length];
        var rnd = new Random();
        while (index < length)
        {
            res[index++] = chars[rnd.Next(startRandom, chars.Length)];
        }

        return new string(res);
    }

    private static string Hashing(string password, short saltSize = 16, short keySize = 32, int interations = 10000,
        char splitChar = '.')
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("Senha não pode ser nula ou vazia");
        }

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            saltSize,
            interations,
            HashAlgorithmName.SHA256
        );
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        var result = $"{interations}{splitChar}{salt}{splitChar}{key}";

        return result;
    }

    private static bool Verify(
        string hash,
        string password,
        short keySize = 32,
        int interations = 10000,
        char splitChar = '.')
    {
        password += Configuration.Secrets.PasswordSaltKey;
        
        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hasInterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        if (hasInterations != interations)
            return false;
        
        using var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            interations,
            HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(keySize);
        
        return keyToCheck.SequenceEqual(key);
    }
}