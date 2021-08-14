using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Demo.SignalR.Server.Middlewares
{
    public class SignalRJwtBearerEvents : JwtBearerEvents
    {
        // We have to hook the OnMessageReceived event in order to
        // allow the JWT authentication handler to read the access
        // token from the query string when a WebSocket or 
        // Server-Sent Events request comes in.

        // Sending the access token in the query string is required due to
        // a limitation in Browser APIs. We restrict it to only calls to the
        // SignalR hub in this code.
        // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
        // for more information about security considerations when using
        // the query string to transmit the access token.

        public override Task MessageReceived(MessageReceivedContext context)
        {
            if(
                context.Request.Query.TryGetValue("access_token", out StringValues accessToken)
                &&
                // If the request is for our hub...
                context.HttpContext.Request.Path.StartsWithSegments("/chat")
                &&
                context.Scheme.Name == JwtBearerDefaults.AuthenticationScheme
            )
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return base.MessageReceived(context);
        }
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return base.AuthenticationFailed(context);
        }
        public override Task Forbidden(ForbiddenContext context)
        {
            return base.Forbidden(context);
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            return base.Challenge(context);
        }

        public override Task TokenValidated(TokenValidatedContext context)
        {
            return base.TokenValidated(context);
        }
    }
}