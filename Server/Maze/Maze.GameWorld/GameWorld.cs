using Maze.Common;
using Maze.GameWorld.Components;
using Maze.GameWorld.System;
using Maze.MazeStructure;
using System;
using System.Collections.Generic;

namespace Maze.GameWorld
{
    public class GameWorld
    {
        internal IMazeInfo MazeInfo { get; }
        internal MazeState State { get; }

        private readonly List<ISystem> _pipeLine;
        private readonly ResultBuilder _builder = new ResultBuilder();

        private Dictionary<Guid, Entity> _players = new Dictionary<Guid, Entity>();

        public GameWorld(IMazeInfo mazeInfo)
        {
            MazeInfo = mazeInfo;
            State = new MazeState();

            _pipeLine = new List<ISystem>()
            {
                new MovementSystem(mazeInfo)
            };
        }

        public Guid CreatePlayer()
        {
            return CreatePlayer(MazeInfo.MazeStructure.HeadRoom.Id);
        }

        public Guid CreatePlayer(int startRoomId)
        {
            Guid playerId = Guid.NewGuid();
            var player = State.CreateEntity();
            State.Add(player, new PlayerTag());
            State.Add(player, new RoomPostition { RoomId = startRoomId });

            _players.Add(playerId, player);

            return playerId;
        }

        public int GetPlayerRoomId(Guid playerId)
        {
            var player = _players[playerId];
            return State.Get<RoomPostition>(player).RoomId;
        }

        public ActionResult ExecuteMove(Guid playerID, MoveDirection dir)
        {
            var player = _players.TryGetValue(playerID, out var p) ? p : throw new Exception("Player not found");

            State.Add(player, new MoveIntent { Direction = dir });

            foreach (var s in _pipeLine)
                s.Run(State);

            var result = _builder.Build(State, player);

            State.Remove<MoveIntent>(player);
            State.ClearEvents();

            return result;
        }
    }
}
