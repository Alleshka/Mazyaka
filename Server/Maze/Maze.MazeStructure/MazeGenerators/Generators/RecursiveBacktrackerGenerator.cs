using Maze.Common;
using Maze.Common.Types;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Topology;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.MazeStructure.MazeGenerators.Generators
{
    public class RecursiveBacktrackerGenerator : IMazeGenerator
    {
        private Random _random = new Random();
        private IMazeBuilder _builder;
        private IMazeTopology _mazeTopology;

        private class CellVisitedStatus
        {
            private Dictionary<EntityId, bool> _cellsVisitStatus;
            private int _notVisitedCount = 0;

            public CellVisitedStatus(int capacity)
            {
                _cellsVisitStatus = new Dictionary<EntityId, bool>(capacity);
            }

            public void Add(EntityId point)
            {
                _cellsVisitStatus.Add(point, false);
                _notVisitedCount++;
            }

            public bool IsVisited(EntityId point)
            {
                return _cellsVisitStatus[point];
            }

            public void Visit(EntityId point)
            {
                _cellsVisitStatus[point] = true;
                _notVisitedCount--;
            }

            public bool HasNotVisitedCells => _notVisitedCount > 0;
        }

        public RecursiveBacktrackerGenerator(IMazeBuilder builder, IMazeTopology mazeTopology)
        {
            _builder = builder;
            _mazeTopology = mazeTopology;
        }

        public IMazeInfo Generate()
        {
            CellVisitedStatus cellVisitedStatus = new CellVisitedStatus(_mazeTopology.Rooms.Count());
            _builder.BuildEmptyMaze();

            foreach (var room in _mazeTopology.Rooms)
            {
                _builder.BuildRoom(room);
                cellVisitedStatus.Add(room.Id);
            }

            foreach (var room in _mazeTopology.Rooms)
            {
                var neighbours = _mazeTopology.GetNeighbours(room);
                foreach (var neighbor in neighbours)
                {
                    if (IsWorldEdge(neighbor.Value))
                    {
                        _builder.BuildBoundary(room, neighbor.Key);
                    }
                    else
                    {
                        _builder.BuildWall(room, neighbor.Key, neighbor.Value);
                    }
                }
            }

            var stackCell = new Stack<IMazeRoom>();
            var curCell = _mazeTopology.GetRandomCell(_random);
            cellVisitedStatus.Visit(curCell.Id);

            while (cellVisitedStatus.HasNotVisitedCells)
            {
                var newCell = GetNotVisitedNeighbor(curCell, _mazeTopology, cellVisitedStatus);
                if (newCell != null)
                {
                    stackCell.Push(curCell);
                    _builder.BuildPassage(curCell, newCell.Value.direction, newCell.Value.room);
                    curCell = newCell.Value.room;
                    cellVisitedStatus.Visit(curCell.Id);
                }
                else
                {
                    if (stackCell.Count != 0)
                    {
                        curCell = stackCell.Pop();
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var exits = _mazeTopology.GetRandomExits(_random);
            foreach (var (direction, room) in exits)
            {
                _builder.BuildExit(room, direction);
            }

            var result = _builder.Build();
            return result;
        }

        private readonly List<(MoveDirection, IMazeRoom)> _neighbourBuffer = new List<(MoveDirection, IMazeRoom)>();
        private (MoveDirection direction, IMazeRoom room)? GetNotVisitedNeighbor(IMazeRoom curCell, IMazeTopology topology, CellVisitedStatus cellsVisitStatus)
        {
            _neighbourBuffer.Clear();
            var neighbours = topology.GetNeighbours(curCell);

            foreach (var neighbor in neighbours)
            {
                if (!IsWorldEdge(neighbor.Value) && !cellsVisitStatus.IsVisited(neighbor.Value.Id))
                {
                    _neighbourBuffer.Add((neighbor.Key, neighbor.Value));
                }
            }

            if (_neighbourBuffer.Count == 0) return null;
            else return _neighbourBuffer[_random.Next(_neighbourBuffer.Count)];
        }

        private bool IsWorldEdge(IMazeRoom room) => room.Id == WorldEdgeSite.Instance.Id;
    }
}
