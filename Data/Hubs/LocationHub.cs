using HvZ_backend.Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace HvZ_backend.Data.Hubs
{
    public class LocationHub : Hub
    {
        private readonly HvZDbContext _context;

        public LocationHub(HvZDbContext context)
        {
            _context = context;
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
        public async Task OnConnectedAsync(int playerId)
        {
            string squadId = DetermineSquadIdForClient(playerId);
            await Groups.AddToGroupAsync(Context.ConnectionId, squadId);
        }

        public async Task SendLocationUpdate(int playerId, int x, int y)
        {
            // Broadcast the location update to all clients in the same squad group
            string squadId = DetermineSquadIdForClient(playerId);
            await Clients.Group(squadId).SendAsync("ReceiveLocationUpdate", playerId, x, y);
        }
    }
}
