using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HvZ_backend.Data.Hubs
{
    public class ChatHub : Hub
    {
        // Allows a user to join a chat room by adding their connection to the specified room's group.
        public async Task JoinRoom(string roomName)
        {
            // Validate if the roomName is one of the allowed lobby names
            if (IsValidLobby(roomName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            }
        }

        // Allows a user to leave a chat room by removing their connection from the specified room's group.
        public async Task LeaveRoom(string roomName)
        {
            // Validate if the roomName is one of the allowed lobby names
            if (IsValidLobby(roomName))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            }
        }

        // Allows a user to send a message to everyone in a specific chat room.
        
        public async Task SendMessage(string user, string message, string roomName)
        {
            // Validate if the roomName is one of the allowed lobby names
            if (IsValidLobby(roomName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message, roomName);
                // Sends the message to all clients in the specified room (group) with "ReceiveMessage" method.

            }
        }
         public async Task SendMessage1(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        // Helper method to validate lobby names
        private bool IsValidLobby(string roomName)
        {
            // Define a list of allowed lobby names
            string[] allowedLobbies = { "Global Chat", "Zombies Chat", "Humans Chat" };
            // Check if the specified roomName is in the list of allowed lobbies
            return allowedLobbies.Contains(roomName);
        }
    }
}
