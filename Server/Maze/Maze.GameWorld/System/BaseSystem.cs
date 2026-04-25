using Maze.GameWorld.Components;
using Maze.MazeStructure;

namespace Maze.GameWorld.System
{
    internal abstract class BaseSystem : ISystem
    {
        public abstract void Run(MazeState world);

        protected IMazeInfo? GetMazeForPlayerOrDefault(Entity entity, MazeState world)
        {
            return world.Has<PlayerMaze>(entity) ? world.Get<PlayerMaze>(entity).MazeInfo : null;
        }
    }
}
