using Maze.Common;
using Maze.Common.Types;
using System.Collections.Generic;

namespace Maze.MazeStructure.MazeSites
{
    public interface IMazeRoom
    {
        public EntityId Id { get; }

        public void AddConnection(MoveDirection direction, IMazeConnection connection);
        public IMazeConnection GetConnection(MoveDirection direction);
        public IReadOnlyDictionary<MoveDirection, IMazeConnection> Connections { get; }
    }
}
