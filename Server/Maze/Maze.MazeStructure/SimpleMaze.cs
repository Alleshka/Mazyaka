using Maze.Common;
using Maze.MazeStructure.MazeSites;
using System;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    public class SimpleMaze : IMaze
    {
        private Dictionary<int, IMazeRoom> _rooms;
        private Dictionary<(int, int), IMazeConnection> _connections;

        public IMazeRoom HeadRoom { get; private set; }

        public SimpleMaze()
        {
            _rooms = new Dictionary<int, IMazeRoom>();
            _connections = new Dictionary<(int, int), IMazeConnection>();
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
            IMazeRoom roomA = null;
            IMazeRoom roomB = null;

            if ((connection.RoomA?.Id ?? Int32.MinValue) < (connection.RoomB?.Id ?? Int32.MinValue))
            {
                roomA = connection.RoomA;
                roomB = connection.RoomB;
            }
            else
            {
                roomA = connection.RoomB;
                roomB = connection.RoomA;
            }

            _connections[(roomA?.Id ?? Int32.MinValue, roomB?.Id ?? Int32.MinValue)] = connection;
        }

        public IMazeRoom GetRoomByID(int id)
        {
            return _rooms[id];
        }
    }
}
