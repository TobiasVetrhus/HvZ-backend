using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HvZ_backend.Data.Hubs
{
    public class ChatHub : Hub
    {
        private static List<string> connectedUsers = new List<string>();
        private static List<string> squadChatUsers = new List<string>();
        private static Dictionary<string, List<string>> lobbyUsers = new Dictionary<string, List<string>>();

        // Allows a user to join a chat room by adding their connection to the specified room's group.
        public async Task JoinRoom(string roomName, string user)
        {
            if (IsValidLobby(roomName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                UpdateUserList(roomName, user, true);
            }
        }

        // Allows a user to leave a chat room by removing their connection from the specified room's group.
        public async Task LeaveRoom(string roomName, string user)
        {
            if (IsValidLobby(roomName))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
                UpdateUserList(roomName, user, false);
            }
        }

        // Allows a user to send a message to everyone in a specific chat room.
        public async Task SendMessage(string user, string message, string roomName)
        {
            if (IsValidLobby(roomName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message, roomName);
                UpdateUserList(user, true);
                // Remove the user from the lobby if they leave it
                lobbyUsers[roomName].Remove(user);
            }
        }

        public async Task SendMessage1(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            UpdateUserList(user, true);
        }

        // Helper method to validate lobby names
        private bool IsValidLobby(string roomName)
        {
            // Define a list of allowed lobby names
            string[] allowedLobbies = { "Global Chat", "Zombies Chat", "Humans Chat", "Squad Chat" };
            // Check if the specified roomName is in the list of allowed lobbies
            return allowedLobbies.Contains(roomName);
        }

        // Method to update the user list and broadcast it to clients
        private void UpdateUserList(string user, bool isConnected)
        {
            if (isConnected && !connectedUsers.Contains(user))
            {
                connectedUsers.Add(user);
            }
            else if (!isConnected && connectedUsers.Contains(user))
            {
                connectedUsers.Remove(user);
            }

            Clients.All.SendAsync("UserListUpdated", connectedUsers);
        }

        // Allows a user to join the Squad Chat by adding their connection to the Squad Chat group.
        public async Task JoinSquadChat(string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Squad Chat");
            squadChatUsers.Add(user);
            UpdateUserList(user, true);
        }

        // Method to update the user list in a specific lobby and broadcast it to clients in that lobby
        private void UpdateUserList(string roomName, string user, bool isConnected)
        {
            if (!IsValidLobby(roomName))
                return;

            if (!lobbyUsers.ContainsKey(roomName))
                lobbyUsers[roomName] = new List<string>();

            if (isConnected && !lobbyUsers[roomName].Contains(user))
            {
                lobbyUsers[roomName].Add(user);
            }
            else if (!isConnected && lobbyUsers[roomName].Contains(user))
            {
                lobbyUsers[roomName].Remove(user);
            }

            // Broadcast the updated user list to clients in the same lobby
            Clients.Group(roomName).SendAsync("UserListUpdated", lobbyUsers[roomName]);
        }

        // Method to remove the user when they disconnect
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var user = connectedUsers.FirstOrDefault(u => u == connectionId);
            if (user != null)
            {
                UpdateUserList(user, false);
                connectedUsers.Remove(user);
            }

            // Remove user from Squad Chat users if they were in Squad Chat
            if (squadChatUsers.Contains(user))
            {
                squadChatUsers.Remove(user);
                UpdateUserList("Squad Chat", user, false);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
