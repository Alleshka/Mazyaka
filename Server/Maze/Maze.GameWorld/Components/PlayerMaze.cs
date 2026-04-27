using Maze.Common.Types;
using Maze.Common;

namespace Maze.GameWorld.Components
{
    internal struct PlayerMaze
    {
        public EntityId MazeId { get; init; }

        public PlayerMaze(EntityId mazeId)
        {
            MazeId = mazeId;
        }
    }
}
