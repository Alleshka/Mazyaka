using Maze.Common;
using Maze.MazeStructure.MazeSites;
using System;
using System.Collections.Generic;

namespace Maze.MazeStructure.Topology
{
    public interface IMazeTopology
    {
        IEnumerable<IMazeRoom> Rooms { get; }
        IReadOnlyDictionary<MoveDirection, IMazeRoom> GetNeighbours(IMazeRoom room);
        IMazeRoom GetNeighbour(IMazeRoom room, MoveDirection direction);
        IMazeRoom GetRandomCell(Random random = null);
        IMazeRoom GetRandomBoundary(MoveDirection? moveDirection, Random random = null);
        MoveDirection DirectionBetween(IMazeRoom a, IMazeRoom b);
        IReadOnlyDictionary<MoveDirection, IMazeRoom> GetRandomExits(Random random = null);
        IMazeRoom GetRoomByCoordinates(int row, int col);
        
    }
}
