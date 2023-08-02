using Maze.Common;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    internal class SimpleMazeRoom : BaseMazeSite, IMazeRoom
    {
        public int Line { get; protected set; }
        public int Column { get; protected set; }
        public bool IsVisited { get; set; } = false;

        private Dictionary<MoveDirection, IMazeSite> _sides;

        public SimpleMazeRoom(int line, int col)
        {
            Line = line;
            Column = col;

            _sides = new Dictionary<MoveDirection, IMazeSite>();
        }

        public IMazeSite GetMazeSite(MoveDirection direction)
        {
            return _sides[direction];
        }

        public void SetMazeSite(MoveDirection direction, IMazeSite site)
        {
            _sides[direction] = site;
        }

        public override MoveResult Enter(IMazePlayer player)
        {
            player.Line = this.Line;
            player.Col = this.Column;

            return base.Enter(player);
        }
    }
}
