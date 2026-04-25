using Maze.Common;
using Maze.Common.DTO;
using Maze.GameWorld.Components;
using Maze.GameWorld.Results;
using Maze.GameWorld.System;
using Maze.MazeStructure;
using System;
using System.Collections.Generic;

namespace Maze.GameWorld
{
    public class GameWorld
    {
        internal MazeState State { get; }

        private readonly List<ISystem> _pipeline;
        private readonly ResultBuilder _builder = new ResultBuilder();

        private Dictionary<Guid, Entity> _players = new Dictionary<Guid, Entity>();

        public GameWorld()
        {
            State = new MazeState();

            _pipeline = new List<ISystem>
            {
                new DestroyWallSystem(),
                new MovementSystem(),
            };
        }

        public Guid CreatePlayer(IMazeInfo mazeInfo, int startRoomId)
        {
            Guid playerId = Guid.NewGuid();
            var player = State.CreateEntity();
            State.Add(player, new PlayerTag());
            State.Add(player, new RoomPosition { RoomId = startRoomId });
            State.Add(player, new PlayerMaze(mazeInfo));
            State.Add(player, new Grenades { Count = 3 });

            _players.Add(playerId, player);

            return playerId;
        }

        public int GetPlayerRoomId(Guid playerId)
        {
            var player = _players[playerId];
            return State.Get<RoomPosition>(player).RoomId;
        }

        public MoveResult ExecuteMove(Guid playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new MoveIntent { Direction = dir });
            return Execute(player).MoveResult!;
        }

        public DestroyWallResult ExecuteDestroyWall(Guid playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new DestroyWallIntent { Direction = dir });
            return Execute(player).DestroyResult!;
        }
        
        private Entity GetPlayer(Guid id) =>  _players.TryGetValue(id, out var p) ? p : throw new Exception("Player not found");

        private ActionResult Execute(Entity player)
        {
            RunPipeline();
            var result = _builder.Build(State, player);
            Clear(State);
            return result;
        }

        private void Clear(MazeState state)
        {
            state.ClearIntents();
            state.ClearEvents();
        }

        private void RunPipeline()
        {
            foreach (var s in _pipeline)
                s.Run(State);
        }
    }
}
