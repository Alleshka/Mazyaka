using Maze.Common;
using Maze.MazeStructure.MazeSites;

namespace Maze.MazeStructure.MazeGenerators
{
    public interface IMazeBuilder
    {
        void BuildEmptyMaze();
        void BuildRoom(IMazeRoom room);
        void BuildPassage(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB);
        void BuildWall(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB);
        void BuildBoundary(IMazeRoom room, MoveDirection direction);
        void BuildExit(IMazeRoom room, MoveDirection direction);
        IMazeInfo Build();
    }
}
