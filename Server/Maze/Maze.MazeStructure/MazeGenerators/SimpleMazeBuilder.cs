using Maze.Common;
using Maze.Common.Types;
using Maze.MazeStructure.Items;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Maze.MazeStructure.MazeGenerators
{
    public class SimpleMazeBuilder : IMazeBuilder
    {
        private IMaze _curMaze;
        private IMazeMetadata _mazeMetadata;
        private readonly Dictionary<EntityId, List<IRoomItem>> _roomItems = new Dictionary<EntityId, List<IRoomItem>>();

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
            IMazeConnection connection = BuildConnection(room, direction, WorldEdgeSite.Instance);
        }

        private IMazeConnection BuildConnection(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB)
        {
            IMazeConnection connection = roomA.GetConnection(direction);
            if (connection != null)
            {
                return connection;
            }
            else
            {
                connection = new BaseMazeConnection(EntityId.New() , roomA, roomB);
                roomA.AddConnection(direction, connection);
                roomB.AddConnection(direction.Opposite(), connection);
                _curMaze.AddConnection(connection);
                return connection;
            }
        }

        public void BuildPassage(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB)
        {
            IMazeConnection connection = BuildConnection(roomA, direction, roomB);
            MarkPassage(connection);
        }

        public void BuildWall(IMazeRoom roomA, MoveDirection direction, IMazeRoom roomB)
        {
            IMazeConnection connection = BuildConnection(roomA, direction, roomB);
            MarkWall(connection);
        }

        public void BuildExit(IMazeRoom room, MoveDirection direction)
        {
            IMazeConnection connection = room.GetConnection(direction);
            if (connection == null)
                return;

            MarkExit(connection);
        }

        public void MarkWall(IMazeConnection connection)
        {
            _mazeMetadata.SetClass(connection, ConnectionClass.Wall);
        }

        public void MarkPassage(IMazeConnection connection)
        {
            _mazeMetadata.SetClass(connection, ConnectionClass.Passage);
        }

        public void MarkExit(IMazeConnection connection)
        {
            // Can only set exit on a boundary connection
            if (!(connection.RoomA is WorldEdgeSite) && !(connection.RoomB is WorldEdgeSite))
                return;

            _mazeMetadata.MarkExit(connection);
        }

        public void PlaceItem(EntityId roomId, IRoomItem item)
        {
            if (!_roomItems.TryGetValue(roomId, out var list))
                _roomItems[roomId] = list = new List<IRoomItem>();
            list.Add(item);
        }

        public IMazeInfo Build()
        {
            var ro = _roomItems.ToDictionary(
                kv => kv.Key,
                kv => (IReadOnlyList<IRoomItem>)kv.Value.AsReadOnly());

            return new SimpleMazeInfo(_curMaze, _mazeMetadata, ro);
        }
    }
}
