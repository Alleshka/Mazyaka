using Maze.Common.Types;
using Maze.MazeStructure.Items;
using System.Collections.Generic;

namespace Maze.GameWorld.Events
{
    public record struct ItemsPickedUpEvent(IEnumerable<IRoomItem> Items);
}
