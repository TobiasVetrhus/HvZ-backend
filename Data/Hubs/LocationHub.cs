using HvZ_backend.Data.Entities;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace HvZ_backend.Data.Hubs
{
    public class LocationHub : Hub
    {
        private readonly HvZDbContext _context;

        public LocationHub(HvZDbContext context)
        {
            _context = context;
        }
        public async Task AddToGroup(int playerId)
        {
            // Perform actions when a client connects, such as adding them to a group
            string squadId = DetermineSquadIdForClient(playerId);
            await Groups.AddToGroupAsync(Context.ConnectionId, squadId);
        }
        private string DetermineSquadIdForClient(int playerId)
        {
            var player = _context.Players.FirstOrDefault(p => p.Id == playerId);
            if (player != null && player.SquadId.HasValue)
            {
                return player.SquadId.Value.ToString();
            }
            return null;
        }

        public async Task SendLocationUpdate(int playerId, int x, int y)
        {
            Log.Information($"playerId: {playerId}, ConnectionId: {Context.ConnectionId}");

            string squadId = DetermineSquadIdForClient(playerId);

            await Groups.AddToGroupAsync(Context.ConnectionId, squadId);
            await Clients.Group(squadId).SendAsync("ReceiveLocationUpdate", playerId, x, y);
        }

    }
}

