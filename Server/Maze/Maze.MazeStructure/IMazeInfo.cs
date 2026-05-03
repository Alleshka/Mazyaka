using Maze.Common.Types;
using Maze.MazeStructure.Items;
using Maze.MazeStructure.Metadata;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    public interface IMazeInfo
    {
        IMaze MazeStructure { get; }
        IMazeMetadata Metadata { get; }
        IReadOnlyDictionary<EntityId, IReadOnlyList<IRoomItem>> RoomItems { get; }
    }
}
