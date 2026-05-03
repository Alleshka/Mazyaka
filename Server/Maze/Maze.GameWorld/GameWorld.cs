using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.GameWorld.Results;
using Maze.GameWorld.System;
using Maze.GameWorld.TraversalPolicies;
using Maze.MazeStructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Maze.GameWorld
{
    public class GameWorld
    {
        internal MazeState State { get; }

        private readonly List<ISystem> _pipeline;
        private readonly ResultBuilder _builder = new ResultBuilder();

        private Dictionary<PlayerId, EcsEntity> _players = new Dictionary<PlayerId, EcsEntity>();
        internal MazeRegistry MazeRegistry { get; private set; } = new MazeRegistry();
        internal GameContext Context { get; private set; }

        public GameWorld()
        {
            State = new MazeState();

            _pipeline = new List<ISystem>
            {
                new DestroyWallSystem(),
                new MovementSystem(),
                new KeyGateSystem(),
                new PickupSystem(),
            };

            Context = new GameContext()
            {
                WorldState = State,
                MazeRuntime = new MazeRuntimeState(),
                Registry = MazeRegistry
            };
        }

        public EntityId RegisterMaze(IMazeInfo mazeInfo)
        {
            EntityId mazeId = MazeRegistry.Register(mazeInfo);
            PlaceItemsFromMaze(mazeId, mazeInfo);
            return mazeId;
        }

        public PlayerId CreatePlayer(EntityId mazeId, EntityId startRoomId, PlayerId playerId = default)
        {
            if (playerId == PlayerId.Empty)
                playerId = PlayerId.New();

            var player = State.CreateEntity();
            State.Add(player, new PlayerTag());
            State.Add(player, new RoomPosition(startRoomId));
            State.Add(player, new PlayerMaze(mazeId));
            State.Add(player, new Grenades { Count = 3 });
            State.Add(player, new Inventory());
            State.Add(player, new TraversalPolicyComponent(DefaultTraversalPolicy.Instance));

            _players.Add(playerId, player);

            return playerId;
        }

        public EntityId GetPlayerRoomId(PlayerId playerId)
        {
            var player = _players[playerId];
            return State.Get<RoomPosition>(player).RoomId;
        }

        // not thread-safe: single player per session
        public MoveResponse ExecuteMove(PlayerId playerID, MoveDirection dir, EntityId? keyId = null)
        {
            var player = GetPlayer(playerID);

            if (keyId == null)
            {
                // Pre-check: pure query, no state mutation. Short-circuit at exit before running the pipeline.
                var ctx =  ConnectionContext.Build(Context, player, dir);
                var traversal = State.Get<TraversalPolicyComponent>(player).Policy.CanTraverse(ctx);
                if (traversal is ExitReached)
                {
                    var inv = State.Has<Inventory>(player) ? State.Get<Inventory>(player) : new Inventory();
                    return MoveResponse.NeedsKey(inv.Items.Select(x=>x.ItemId).ToList());
                }
                // Cache so MovementSystem does not re-evaluate traversal.
                State.Add(player, new CachedTraversalResult(traversal));
            }

            State.Add(player, new MoveIntent(dir, keyId));
            return Execute(player).MoveResult!;
        }

        // not thread-safe: single player per session
        public DestroyWallResult ExecuteDestroyWall(PlayerId playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new DestroyWallIntent { Direction = dir });
            return Execute(player).DestroyResult!;
        }

        private void PlaceItemsFromMaze(EntityId mazeId, IMazeInfo mazeInfo)
        {

            foreach (var (roomId, items) in mazeInfo.RoomItems)
            {
                var key = new MazeRuntimeKey(mazeId, roomId);
                Context.MazeRuntime.RoomItems[key] = items.ToList();
            }
        }

        private EcsEntity GetPlayer(PlayerId id) => _players.TryGetValue(id, out var p) ? p : throw new Exception("Player not found");

        private ActionResult Execute(EcsEntity player)
        {
            RunPipeline();

            if (State.Has<ExceptionEvent>(player))
            {
                var ex = State.Get<ExceptionEvent>(player);
                throw new Exception(ex.message);
            }

            var result = _builder.Build(State, player);
            Clear(State);
            return result;
        }

        private void Clear(MazeState state)
        {
            state.ClearIntents();
            state.ClearEvents();
            state.ClearCache();
        }

        private void RunPipeline()
        {
            foreach (var s in _pipeline)
                s.Run(Context);
        }
    }
}
