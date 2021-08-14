using Demo.SignalR.Server.DTOs;
using Demo.SignalR.Server.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Demo.SignalR.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public MessagesController(
            IHubContext<ChatHub, IChatHub> hubContext
        )
        {
            this._hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(MessageRequest request)
        {
            await this._hubContext
                    .Clients.All
                        .OnMessage(new Message(request.Text));

            return this.NoContent();
        }
    }
}