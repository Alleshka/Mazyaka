using Maze.Common.Types;
using Maze.MazeStructure.MazeSites;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    public interface IMaze
    {
        void AddRoom(IMazeRoom room);
        void AddConnection(IMazeConnection connection);

        IMazeRoom HeadRoom { get; }
        IMazeRoom GetRoomByID (EntityId id);
        IEnumerable<IMazeRoom> Rooms { get; }
    }
}
