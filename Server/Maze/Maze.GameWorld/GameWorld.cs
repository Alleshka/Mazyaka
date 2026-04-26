using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
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

        private Dictionary<EntityId, Entity> _players = new Dictionary<EntityId, Entity>();

        public GameWorld()
        {
            State = new MazeState();

            _pipeline = new List<ISystem>
            {
                new DestroyWallSystem(),
                new MovementSystem(),
            };
        }

        public EntityId CreatePlayer(IMazeInfo mazeInfo, EntityId startRoomId)
        {
            EntityId playerId = EntityId.New();
            var player = State.CreateEntity();
            State.Add(player, new PlayerTag());
            State.Add(player, new RoomPosition { RoomId = startRoomId });
            State.Add(player, new PlayerMaze(mazeInfo));
            State.Add(player, new Grenades { Count = 3 });

            _players.Add(playerId, player);

            return playerId;
        }

        public EntityId GetPlayerRoomId(EntityId playerId)
        {
            var player = _players[playerId];
            return State.Get<RoomPosition>(player).RoomId;
        }

        // not thread-safe: single player per session
        public MoveResult ExecuteMove(EntityId playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new MoveIntent { Direction = dir });
            return Execute(player).MoveResult!;
        }

        // not thread-safe: single player per session
        public DestroyWallResult ExecuteDestroyWall(EntityId playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new DestroyWallIntent { Direction = dir });
            return Execute(player).DestroyResult!;
        }
        
        private Entity GetPlayer(EntityId id) =>  _players.TryGetValue(id, out var p) ? p : throw new Exception("Player not found");

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
