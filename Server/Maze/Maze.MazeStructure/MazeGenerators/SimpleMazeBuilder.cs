using Maze.Common;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;

namespace Maze.MazeStructure.MazeGenerators
{
    class SimpleMazeBuilder : IMazeBuilder
    {
        private IMaze _curMaze;
        private IMazeMetadata _mazeMetadata;
        private int _connectionId = 0;

        public void BuildEmptyMaze()
        {
            _curMaze = new SimpleMaze();
            _mazeMetadata = new MazeMetadata();
        }

        public void BuildRoom(IMazeRoom room)
        {
            _curMaze.AddRoom(room);
        }

        public void BuildBoundary(IMazeRoom room, MoveDirection direction)
        {
            IMazeConnection connection = new BaseMazeConnection(_connectionId++, room, null);
            room.AddConnection(direction, connection);
            _mazeMetadata.AddTypeTag(connection, ConnectionTypeTag.Boundary);
            _curMaze.AddConnection(connection);
        }

        public void BuildPassage(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB)
        {
            IMazeConnection connection = new BaseMazeConnection(_connectionId++, roomA, roomB);
            roomA.AddConnection(direction, connection);
            roomB.AddConnection(direction.Opposite(), connection);
            _mazeMetadata.AddTypeTag(connection, ConnectionTypeTag.Passage);
            _curMaze.AddConnection(connection);
        }

        public void BuildWall(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB)
        {
            IMazeConnection connection = new BaseMazeConnection(_connectionId++, roomA, roomB);
            roomA.AddConnection(direction, connection);
            roomB.AddConnection(direction.Opposite(), connection);
            _curMaze.AddConnection(connection);
        }

        public void BuildExit(IMazeRoom room, MoveDirection direction)
        {
            IMazeConnection connection = room.GetConnection(direction);
            if (connection == null)
            {
                return;
            }

            // Can set Tags only if Boundary
            if (!_mazeMetadata.HasTypeTag(connection, ConnectionTypeTag.Boundary))
            {
                return;
            }
            ;

            _mazeMetadata.AddTypeTag(connection, ConnectionTypeTag.Exit);
            _mazeMetadata.RemoveTypeTag(connection, ConnectionTypeTag.Boundary);
        }

        public IMazeInfo Build()
        {
            return new SimpleMazeInfo(_curMaze, _mazeMetadata);
        }
    }
}
