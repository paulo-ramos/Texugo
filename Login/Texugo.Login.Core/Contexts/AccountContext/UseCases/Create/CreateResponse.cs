using Flunt.Notifications;
using Texugo.Login.Core.Contexts.SharedContext.UseCases;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Create;

public class CreateResponse : BaseResponse
{
    protected CreateResponse()
    {
    }

    public CreateResponse(string message, int status, IEnumerable<Notification>? notifications=null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public CreateResponse(string message, ResponseData data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
    }
    public ResponseData? Data { get; private set; }
}

public record ResponseData(Guid Id, string Name, string Email);