using Maze.Core.Common;
using Maze.MazeStructure;
using Maze.MazeStructure.Interfaces;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic
{
    public interface IMazeGame
    {
        void SetMaze(IMaze maze);

        IMazeRoom SetPlayer(MazePoint point);
        IMazeRoom SetPlayer(int line, int col);

        MoveResult MovePlayer(Guid userId, MoveDirection direction);
    }
}