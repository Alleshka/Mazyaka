using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal class SimpleMazePassage : BaseMazeSite, IMazePassage
    {
        public override MoveResult Enter(IMazePlayer player, MoveDirection direction)
        {
            var fromRoom = this[direction.Opposite()] as IMazeRoom;
            var toRoom = this[direction] as IMazeRoom;

            fromRoom.RemoveCharacter(player);
            toRoom.AddCharacter(player);

            return toRoom.Enter(player, direction);
        }
    }
}
