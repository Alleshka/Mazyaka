using Maze.Common.MazeStructure.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure
{
    public class BaseCell : BaseBlock, ICell
    {
        private Dictionary<IMazeDirection, IMazeBlock> _links = new Dictionary<IMazeDirection, IMazeBlock>();

        public IMazeBlock this[IMazeDirection direction] => GetBlock(direction);

        public IMazeBlock GetBlock(IMazeDirection direction)
        {
            _links.TryGetValue(direction, out var block);
            return block;
        }

        public void SetBlock(IMazeDirection direction, IMazeBlock block)
        {
            _links[direction] = block;
        }
    }
}
