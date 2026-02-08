using Maze.Common;
using Maze.MazeStructure.MazeSites;

namespace Maze.MazeStructure
{
    public interface IMaze
    {
        void AddRoom(IMazeRoom room);
        void AddConnection(IMazeConnection connection);

        IMazeRoom HeadRoom { get; }
        IMazeRoom GetRoomByID (int id);
    }
}
