using Maze.MazeStructure.Items;
using System.Collections.Generic;
using System.Linq;

namespace Maze.GameWorld.Components
{
    public class Inventory
    {
        public List<IRoomItem> Items { get; } = new();

        public bool Has<T>() where T : IRoomItem => Items.OfType<T>().Any();

        public List<T> Get<T>() where T : IRoomItem => Items.OfType<T>().ToList();

    }
}
