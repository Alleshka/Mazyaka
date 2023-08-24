using Maze.Common;
using Maze.MazeStructure.Interfaces;

namespace Maze.GameLogic
{
    public interface IMazeGame
    {
        public IMaze CreateMaze(IMazeGenerator generator, IMazeBuilder mazeBuilder);

        public Result SetPlayer(MazePoint point);
        public Result SetPlayer(int line, int col);

        public Result<MoveResult> MovePlayer(Guid userId, MoveDirection direction);

        public Result DestroyRoom(Guid userId, MoveDirection direction);
    }
}