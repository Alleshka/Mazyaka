using Maze.Common.Types;
using Maze.MazeStructure.Items;
using Maze.MazeStructure.Metadata;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    internal class SimpleMazeInfo : IMazeInfo
    {
        public IMaze MazeStructure { get; }
        public IMazeMetadata Metadata { get; }
        public IReadOnlyDictionary<EntityId, IReadOnlyList<IRoomItem>> RoomItems { get; }

        public SimpleMazeInfo(IMaze mazeStructure, IMazeMetadata metadata, IReadOnlyDictionary<EntityId, IReadOnlyList<IRoomItem>> roomItems)
        {
            MazeStructure = mazeStructure;
            Metadata = metadata;
            RoomItems = roomItems;
        }
    }
}
