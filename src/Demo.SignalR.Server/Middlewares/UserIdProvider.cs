using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Demo.SignalR.Server.Middlewares
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}