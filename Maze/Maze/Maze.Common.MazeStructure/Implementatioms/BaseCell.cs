using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;
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
        private Dictionary<Guid, IGameObject> _objects = new Dictionary<Guid, IGameObject>();

        public IMazeBlock this[IMazeDirection direction] => GetBlock(direction);

        public void AddObject(IGameObject gameObject)
        {
            _objects.Add(gameObject.ObjectID, gameObject);
        }

        public void Execute(ILiveGameObject gameObject)
        {
            foreach(var obj in _objects)
            {
                obj.Value.Execute(gameObject);
            }
        }

        public IMazeBlock GetBlock(IMazeDirection direction)
        {
            _links.TryGetValue(direction, out var block);
            return block;
        }

        public void RemoveObject(ILiveGameObject gameObject)
        {
            if (_objects.ContainsKey(gameObject.ObjectID))
            {
                _objects.Remove(gameObject.ObjectID);
            }
        }

        public void SetBlock(IMazeDirection direction, IMazeBlock block)
        {
            _links[direction] = block;
        }

        public override void MoveObject(ILiveGameObject gameObject, IMazeDirection direction)
        {
            var block = GetBlock(direction);
            block.MoveObject(gameObject, direction);
        }
    }
}
