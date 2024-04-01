using System.Data.SqlTypes;
using Flunt.Notifications;

namespace Texugo.Login.Core.Contexts.SharedContext.UseCases;

public abstract class BaseResponse
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; } = 400;
    public bool IsSuccess => Status is >= 200 and <= 299;
    public IEnumerable<Notification>? Notifications { get; set; }
}