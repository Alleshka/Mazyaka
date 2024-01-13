using Maze.Common;
using Maze.MazeStructure.Interfaces;

namespace Maze.GameLogic
{
    public interface IMazeGame
    {
        public IMaze CreateMaze(IMazeGenerator generator, IMazeBuilder mazeBuilder);

        public void SetPlayer(MazePoint point);
        public void SetPlayer(int line, int col);

        public MoveResult MovePlayer(Guid userId, MoveDirection direction);

        public void DestroyRoom(Guid userId, MoveDirection direction);
    }
}