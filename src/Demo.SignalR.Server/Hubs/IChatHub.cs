using Demo.SignalR.Server.DTOs;
using System.Threading.Tasks;

namespace Demo.SignalR.Server.Hubs
{
    public interface IChatHub
    {
        Task OnMessage(Message message);
    }
}