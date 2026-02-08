using Maze.Common;
using Maze.GameLogic.Connections;
using Maze.GameLogic.GameMazeSite;
using Maze.MazeStructure;

namespace Maze.GameLogic.Actors
{
    public class Player : IActor
    {
        public GameMazeRoom CurrentRoom { get; private set; }

        public Player(GameMazeRoom startRoom)
        {
            CurrentRoom = startRoom;
        }

        public MoveResult Move(MoveDirection direction)
        {
            
        }

        public MoveResult TryMove(IMazeGameConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
