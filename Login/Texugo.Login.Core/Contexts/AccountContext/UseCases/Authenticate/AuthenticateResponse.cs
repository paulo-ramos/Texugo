using Flunt.Notifications;
using Texugo.Login.Core.Contexts.SharedContext.UseCases;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public class AuthenticateResponse : BaseResponse
{
    protected AuthenticateResponse()
    {
    }

    public AuthenticateResponse(string message, int status, IEnumerable<Notification>? notifications=null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public AuthenticateResponse(string message, ResponseData data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
    }
    public ResponseData? Data { get; private set; }
}

public class ResponseData
{
    public string Token { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}
