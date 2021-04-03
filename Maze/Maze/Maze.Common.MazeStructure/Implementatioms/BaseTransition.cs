using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure
{
    public class BaseTransition : BaseBlock, ITransition
    {
        private ICell _source;
        private ICell _destination;

        public BaseTransition(ICell startcell, ICell nextCell)
        {
            _source = startcell;
            _destination = nextCell;
        }

        public override void MoveObject(ILiveGameObject gameObject, IMazeDirection direction)
        {
            base.MoveObject(gameObject, direction);

            Logging.MazeLogManager.Debug("Переход между ячейками");
            _source.RemoveObject(gameObject);
            _destination.Execute(gameObject);
            _destination.AddObject(gameObject);
        }
    }
}
