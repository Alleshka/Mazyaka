using Maze.Common.Types;

namespace Maze.MazeStructure.MazeSites
{
    public interface IMazeConnection
    {
        public EntityId Id { get; }
        public IMazeRoom RoomA { get; }
        public IMazeRoom RoomB { get; }

        public IMazeRoom GetOther(IMazeRoom room);
    }
}
