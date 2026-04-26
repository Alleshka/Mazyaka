using Maze.Common;
using Maze.Common.Types;
using Maze.MazeStructure.MazeSites;
using System;
using System.Collections.Generic;
using System.IO;

namespace Maze.MazeStructure.Topology
{
    public class GridTopology : IMazeTopology
    {
        private Random _random = new Random();

        private readonly int _rowCount;
        private readonly int _colCount;

        private readonly IMazeRoom[,] _rooms;
        private readonly Dictionary<IMazeRoom, (int row, int col)> _roomPositions = new Dictionary<IMazeRoom, (int row, int col)>();

        public GridTopology(int rowCount, int colCount)
        {
            _rowCount = rowCount;
            _colCount = colCount;
            _rooms = new IMazeRoom[_rowCount, _colCount];

            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _colCount; j++)
                {
                    IMazeRoom room = new BaseMazeRoom(EntityId.New());
                    _rooms[i, j] = room;
                    _roomPositions[room] = (i, j);
                }
            }
        }

        public IEnumerable<IMazeRoom> Rooms
        {
            get
            {
                for (int i = 0; i < _rowCount; i++)
                {
                    for (int j = 0; j < _colCount; j++)
                    {
                        yield return _rooms[i, j];
                    }
                }
            }
        }

        public IMazeRoom GetNeighbour(IMazeRoom room, MoveDirection direction) => GetNeighbours(room)[direction];

        public IReadOnlyDictionary<MoveDirection, IMazeRoom> GetNeighbours(IMazeRoom room)
        {
            (int row, int col) = _roomPositions[room];
            var neighbours = new Dictionary<MoveDirection, IMazeRoom>();

            neighbours[MoveDirection.Up] = row > 0 ? _rooms[row - 1, col] : WorldEdgeSite.Instance;
            neighbours[MoveDirection.Down] = row < _rowCount - 1 ? _rooms[row + 1, col] : WorldEdgeSite.Instance;
            neighbours[MoveDirection.Left] = col > 0 ? _rooms[row, col - 1] : WorldEdgeSite.Instance;
            neighbours[MoveDirection.Right] = col < _colCount - 1 ? _rooms[row, col + 1] : WorldEdgeSite.Instance;

            return neighbours;
        }

        public IMazeRoom GetRandomCell(Random random = null)
        {
            if (random == null) random = _random;

            int curRow = random.Next(_rowCount);
            int curCol = random.Next(_colCount);

            return _rooms[curRow, curCol];
        }

        public MoveDirection DirectionBetween(IMazeRoom a, IMazeRoom b)
        {
            MoveDirection moveDirection = MoveDirection.None;

            (int row, int col) room1 = _roomPositions[a];
            (int row, int col) room2 = _roomPositions[b];

            if (room1.row == room2.row)
            {
                if (room1.col < room2.col)
                {
                    moveDirection = MoveDirection.Right;
                }
                else if (room1.col > room2.col)
                {
                    moveDirection = MoveDirection.Left;
                }
            }
            else if (room1.col == room2.col)
            {
                if (room1.row < room2.row)
                {
                    moveDirection = MoveDirection.Down;
                }
                else if (room1.row > room2.row)
                {
                    moveDirection = MoveDirection.Up;
                }
            }

            return moveDirection;
        }

        public IMazeRoom GetRandomBoundary(MoveDirection? direction, Random random = null)
        {
            MoveDirection dir = MoveDirection.None;
            if (random == null) random = _random;

            if (direction != null && direction != MoveDirection.None)
            {
                dir = direction.Value;
            }
            else
            {
                var directions = new[] { MoveDirection.Up, MoveDirection.Right, MoveDirection.Down, MoveDirection.Left };
                dir = directions[random.Next(directions.Length)];
            }

            if (dir == MoveDirection.Up || dir == MoveDirection.Down)
            {
                int randCol = random.Next(_colCount);

                if (dir == MoveDirection.Up) return _rooms[0, randCol];
                else return _rooms[_rowCount - 1, randCol];
            }

            if (dir == MoveDirection.Right || dir == MoveDirection.Left)
            {
                int randRow = random.Next(_rowCount);

                if (dir == MoveDirection.Right) return _rooms[randRow, _colCount - 1];
                else return _rooms[randRow, 0];
            }

            return _rooms[0, 0];
        }

        public IReadOnlyDictionary<MoveDirection, IMazeRoom> GetRandomExits(Random random = null)
        {
            if (random == null) random = _random;

            var directions = new[] { MoveDirection.Up, MoveDirection.Right, MoveDirection.Down, MoveDirection.Left };
            var result = new Dictionary<MoveDirection, IMazeRoom>();

            foreach (var dir in directions)
            {
                result.Add(dir, GetRandomBoundary(dir));
            }

            return result;
        }

        public IMazeRoom GetRoomByCoordinates(int row, int col)
        {
            return _rooms[row, col];
        }
    }
}
