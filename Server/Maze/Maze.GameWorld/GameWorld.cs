using Maze.Common;
using Maze.GameWorld.Components;
using Maze.GameWorld.System;
using Maze.MazeStructure;

namespace Maze.GameWorld
{
    public class GameWorld
    {
        internal IMazeInfo MazeInfo { get; }
        internal MazeState State { get; }

        private readonly MovementSystem _movementSystem;
        private readonly RoomEnterSystem _roomEnterSystem;
        private readonly ExitEnterSystem _exitEnterSystem;

        private Entity _player;

        public GameWorld(IMazeInfo mazeInfo)
        {
            MazeInfo = mazeInfo;
            State = new MazeState();

            _movementSystem = new MovementSystem(this);
            _roomEnterSystem = new RoomEnterSystem();
            _exitEnterSystem = new ExitEnterSystem();

            _player = State.CreateEntity();
            State.Add(_player, new PlayerTag());
            State.Add(_player, new RoomPostition { RoomId = mazeInfo.MazeStructure.HeadRoom.Id });
        }

        public void Move(MoveDirection direction)
        {
            _movementSystem.TryMove(_player, direction);
            _roomEnterSystem.Process(this.State);
            _exitEnterSystem.Process(this.State);
        }
    }
}
