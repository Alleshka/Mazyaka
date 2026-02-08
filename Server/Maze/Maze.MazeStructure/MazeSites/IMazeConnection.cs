namespace Maze.MazeStructure.MazeSites
{
    public interface IMazeConnection
    {
        public IMazeRoom RoomA { get; }
        public IMazeRoom RoomB { get; }

        public IMazeRoom GetOther(IMazeRoom room);
    }
}
