using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.MazeStructure.MazeGenerators.Decorators
{
    public class BraidMazeGenerator : IMazeGenerator
    {
        private readonly IMazeGenerator _inner;
        private readonly IMazeBuilder _builder;
        private readonly float _braidFactor;
        private readonly Random _random = new Random();

        public BraidMazeGenerator(IMazeGenerator inner, IMazeBuilder builder, float braidFactor = 0.5f)
        {
            _inner = inner;
            _builder = builder;
            _braidFactor = braidFactor;
        }

        public IMazeInfo Generate()
        {
            var mazeInfo = _inner.Generate();
                
            if (_braidFactor == 0f) return mazeInfo;

            var deadEnds = FindDeadEnds(mazeInfo);
            deadEnds = deadEnds.OrderBy(_ => _random.Next()).ToList();

            int toBreak = (int)Math.Round(deadEnds.Count * _braidFactor);

            foreach (var room in deadEnds.Take(toBreak))
            {
                var walls = GetWallConnections(room, mazeInfo.Metadata);
                if (walls.Count == 0) continue;

                var target = walls.FirstOrDefault(w => IsDeadEnd(w.GetOther(room), mazeInfo.Metadata)) ?? walls[_random.Next(walls.Count)];
                _builder.MarkPassage(target);
            }

            return mazeInfo;
        }

        private List<IMazeRoom> FindDeadEnds(IMazeInfo mazeInfo)
        {
            var deadEnds = new List<IMazeRoom>();
            foreach (var r in mazeInfo.MazeStructure.Rooms)
            {
                if (IsDeadEnd(r, mazeInfo.Metadata))
                    deadEnds.Add(r);
            }
            return deadEnds;
        }

        private bool IsDeadEnd(IMazeRoom room, IMazeMetadata metadata)
        {
            int passages = room.Connections
                .Count(c => metadata.GetClass(c.Value) == ConnectionClass.Passage);
            return passages == 1;
        }

        private List<IMazeConnection> GetWallConnections(IMazeRoom room, IMazeMetadata metadata)
        {
            return room.Connections.Values
                .Where(c => metadata.GetClass(c) == ConnectionClass.Wall)
                .ToList();
        }
    }
}
