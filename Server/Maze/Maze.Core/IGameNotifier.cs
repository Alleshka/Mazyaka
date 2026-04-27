using System.Threading.Tasks;
using Maze.Common.Types;

namespace Maze.Core
{
    public interface IGameNotifier
    {
        Task BroadcastOpponentMove(string sessionId, PlayerId movingPlayerId); // TODO: Introduce opponent move
        Task BroadcastSessionEnded(string sessionId);

    }
}