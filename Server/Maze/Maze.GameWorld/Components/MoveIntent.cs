using Maze.Common;
using Maze.Common.Types;

namespace Maze.GameWorld.Components
{
    public record struct MoveIntent(MoveDirection Direction, EntityId? KeyId = null);
}
