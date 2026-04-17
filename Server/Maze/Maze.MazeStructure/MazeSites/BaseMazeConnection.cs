using System;

namespace Maze.MazeStructure.MazeSites
{
    public class BaseMazeConnection : IMazeConnection
    {
        public IMazeRoom RoomA { get; }

        public IMazeRoom RoomB { get; }

        public int Id { get; }

        public BaseMazeConnection(int id, IMazeRoom roomA, IMazeRoom roomB)
        {
            Id = id;
            RoomA = roomA;
            RoomB = roomB;
        }

        public override string ToString()
        {
            return $"{(RoomA?.Id)}-{RoomB?.Id}";
        }

        public IMazeRoom GetOther(IMazeRoom room)
        {
            if (room == RoomA)
            {
                return RoomB;
            }
            else if (room == RoomB)
            {
                return RoomA;
            }
            else
            {
                throw new ArgumentException($"The room {room} is not connected by this connection.");
            }
        }
    }
}
