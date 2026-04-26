using Maze.Common.Types;

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
