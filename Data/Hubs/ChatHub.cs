using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HvZ_backend.Data.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessage(string user, string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message, roomName);
        }
    }
}
