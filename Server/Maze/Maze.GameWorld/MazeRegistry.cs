using Maze.Common.Types;
using Maze.MazeStructure;
using System.Collections.Generic;

namespace Maze.GameWorld
{
    internal class MazeRegistry
    {
        public MazeRegistry()
        {

        }

        private readonly Dictionary<EntityId, IMazeInfo> _mazes = new Dictionary<EntityId, IMazeInfo>();

        public EntityId Register(IMazeInfo info)
        {
            var id = EntityId.New();
            _mazes[id] = info;
            return id;
        }

        public IMazeInfo Get(EntityId id) => _mazes[id];
    }
}
