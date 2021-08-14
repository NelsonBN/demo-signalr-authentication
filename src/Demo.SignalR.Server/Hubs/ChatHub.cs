using Demo.SignalR.Server.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Demo.SignalR.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            this._logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            this._logger.LogInformation($"UserId: {this.Context.UserIdentifier}, ConnectionId: '{this.Context.ConnectionId}' connected");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if(string.IsNullOrWhiteSpace(exception?.Message))
            {
                this._logger.LogInformation($"UserId: {this.Context.UserIdentifier}, ConnectionId: '{this.Context.ConnectionId}' disconnected");
            }
            else
            {
                this._logger.LogInformation($"UserId: {this.Context.UserIdentifier}, ConnectionId: '{this.Context.ConnectionId}' disconnected > {exception.Message}");
            }

            return base.OnDisconnectedAsync(exception);
        }
        
        public async Task OnMessage(string clientMessage)
        {
            this._logger.LogInformation($"UserId: {this.Context.UserIdentifier}, ConnectionId: '{this.Context.ConnectionId}' message: {clientMessage}");

            await this.Clients
                .Client(this.Context.ConnectionId)
                    .OnMessage(new Message(clientMessage));
        }
    }
}