using Maze.Common.Types;
using Maze.Core;
using Maze.Core.ConnectionRegistry;
using Maze.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Maze.Server
{
    internal class SignalRGameNotifier : IGameNotifier
    {
        private readonly IHubContext<MazeHub> _hubContext;
        private readonly IConnectionRegistry _registry;

        public SignalRGameNotifier(IHubContext<MazeHub> hubContext, IConnectionRegistry registry)
        {
            _hubContext = hubContext;
            _registry = registry;
        }

        public async Task BroadcastOpponentMove(string sessionId, PlayerId movingPlayerId)
        {
            throw new NotImplementedException();
            //var connection = _registry.GetConnectionId(movingPlayerId);
            //await _hubContext.Clients.GroupExcept(sessionId.Value, movingPlayerId).SendAsync("OpponentMoved", movingPlayerId);
        }

        public async Task BroadcastSessionEnded(string sessionId)
        {
            await _hubContext.Clients.Group(sessionId).SendAsync("SessionEnded");
        }
    }
}
