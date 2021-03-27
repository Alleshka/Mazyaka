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

        public override bool CanMove => true;

        public override void MoveAction()
        {
            base.MoveAction();

            // TODO: _source.RemoveObject
            Logging.MazeLogManager.Debug("Переход между ячейками");
            // TODO: _destination.AddObject
        }
    }
}
