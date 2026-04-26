using Maze.Common.Types;
using Maze.MazeStructure.MazeSites;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    public class SimpleMaze : IMaze
    {
        private Dictionary<EntityId, IMazeRoom> _rooms;
        private Dictionary<(EntityId, EntityId), IMazeConnection> _connections;

        public IMazeRoom HeadRoom { get; private set; }

        public IEnumerable<IMazeRoom> Rooms => _rooms.Values;

        public SimpleMaze()
        {
            _rooms = new Dictionary<EntityId, IMazeRoom>();
            _connections = new Dictionary<(EntityId, EntityId), IMazeConnection>();
        }

        public void AddRoom(IMazeRoom room)
        {
            _rooms[room.Id] = room;
            if (HeadRoom == null)
            {
                HeadRoom = room;
            }
        }

        public void AddConnection(IMazeConnection connection)
        {
            IMazeRoom roomA = connection.RoomA;
            IMazeRoom roomB = connection.RoomB;

            if (connection.RoomA.Id <= connection.RoomB.Id)
            {
                roomA = connection.RoomA;
                roomB = connection.RoomB;
            }
            else
            {
                roomA = connection.RoomB;
                roomB = connection.RoomA;
            }

            _connections[(roomA.Id, roomB.Id)] = connection;
        }

        public IMazeRoom GetRoomByID(EntityId id)
        {
            return _rooms[id];
        }
    }
}
