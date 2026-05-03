using Maze.Common;
using Maze.Common.Types;
using Maze.MazeStructure.Items;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;

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
        void MarkWall(IMazeConnection connection);
        void MarkPassage(IMazeConnection connection);
        void MarkExit(IMazeConnection connection);
        void PlaceItem(EntityId roomId, IRoomItem item);

        IMazeInfo Build();
    }
}
