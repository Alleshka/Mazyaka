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

        private Dictionary<PlayerId, Entity> _players = new Dictionary<PlayerId, Entity>();
        internal MazeRegistry MazeRegistry { get; private set; } = new MazeRegistry();
        internal GameContext Context { get; private set; }

        public GameWorld()
        {
            State = new MazeState();

            _pipeline = new List<ISystem>
            {
                new DestroyWallSystem(),
                new MovementSystem(),
            };

            Context = new GameContext()
            {
                State = State,
                Registry = MazeRegistry
            };
        }

        public PlayerId CreatePlayer(IMazeInfo mazeInfo, EntityId startRoomId, PlayerId playerId)
        {
            EntityId mazeId = MazeRegistry.Register(mazeInfo);

            if (playerId == PlayerId.Empty)
            {
                playerId = PlayerId.New();
            }

            var player = State.CreateEntity();
            State.Add(player, new PlayerTag());
            State.Add(player, new RoomPosition { RoomId = startRoomId });
            State.Add(player, new PlayerMaze(mazeId));
            State.Add(player, new Grenades { Count = 3 });

            _players.Add(playerId, player);

            return playerId;
        }

        public EntityId GetPlayerRoomId(PlayerId playerId)
        {
            var player = _players[playerId];
            return State.Get<RoomPosition>(player).RoomId;
        }

        // not thread-safe: single player per session
        public MoveResult ExecuteMove(PlayerId playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new MoveIntent { Direction = dir });
            return Execute(player).MoveResult!;
        }

        // not thread-safe: single player per session
        public DestroyWallResult ExecuteDestroyWall(PlayerId playerID, MoveDirection dir)
        {
            var player = GetPlayer(playerID);
            State.Add(player, new DestroyWallIntent { Direction = dir });
            return Execute(player).DestroyResult!;
        }
        
        private Entity GetPlayer(PlayerId id) =>  _players.TryGetValue(id, out var p) ? p : throw new Exception("Player not found");

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
                s.Run(Context);
        }
    }
}
