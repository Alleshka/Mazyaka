using System;

namespace Maze.MazeStructure.MazeSites
{
    public class BaseMazeConnection : IMazeConnection
    {
        public IMazeRoom RoomA { get; }

        public IMazeRoom RoomB { get; }

        public BaseMazeConnection(IMazeRoom roomA, IMazeRoom roomB)
        {
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
