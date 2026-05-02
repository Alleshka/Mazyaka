using Maze.GameWorld.Components;
using Maze.MazeStructure;

namespace Maze.GameWorld.System
{
    internal abstract class BaseSystem : ISystem
    {
        public abstract void Run(GameContext gameContext);

        protected IMazeInfo GetMazeForPlayerOrDefault(Entity entity, MazeState world, MazeRegistry mazeRegistry)
        {
            if (!world.Has<PlayerMaze>(entity)) return null;
            return mazeRegistry.Get(world.Get<PlayerMaze>(entity).MazeId);
        }
    }
}
