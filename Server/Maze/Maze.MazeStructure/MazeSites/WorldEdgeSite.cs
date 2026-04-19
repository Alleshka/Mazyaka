using Maze.Common;

namespace Maze.MazeStructure.MazeSites
{
    // Null-object for the "outside" side of boundary connections.
    // Replaces null so GetOther() never throws on boundary connections.
    public sealed class WorldEdgeSite : IMazeRoom
    {
        public static readonly WorldEdgeSite Instance = new WorldEdgeSite();
        private WorldEdgeSite() { }

        public int Id => -1;
        public void AddConnection(MoveDirection direction, IMazeConnection connection) { }
        public IMazeConnection? GetConnection(MoveDirection direction) => null;
    }
}
