using Maze.Common.Types;
using Maze.MazeStructure.Items;
using System;
using System.Linq;

namespace Maze.MazeStructure.MazeGenerators.Decorators
{
    public class KeyPlacementGenerator : IMazeGenerator
    {
        private readonly IMazeGenerator _inner;
        private readonly IMazeBuilder _builder;
        private readonly int _keyCount;

        public KeyPlacementGenerator(IMazeGenerator inner, IMazeBuilder builder, int keyCount = 4)
        {
            _inner = inner;
            _builder = builder;
            _keyCount = keyCount;
        }

        public IMazeInfo Generate()
        {
            var mazeInfo = _inner.Generate();
            var rooms = mazeInfo.MazeStructure.Rooms.ToList();

            if (rooms.Count == 0)
                return mazeInfo;

            var rng = new Random();
            var picked = rooms.OrderBy(_ => rng.Next()).Take(_keyCount).ToList();
            var realIndex = rng.Next(picked.Count);

            for (int i = 0; i < picked.Count; i++)
            {
                var room = picked[i];
                IRoomItem key = new KeyRoomItem(EntityId.New(), room.Id, IsReal: i == realIndex);
                _builder.PlaceItem(room.Id, key);
            }

            return _builder.Build();
        }
    }
}
