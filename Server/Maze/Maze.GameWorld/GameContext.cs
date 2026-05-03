namespace Maze.GameWorld
{
    internal class GameContext
    {
        public MazeState WorldState{ get; set; }
        public MazeRuntimeState MazeRuntime { get; set; }
        public MazeRegistry Registry{ get; set; }
    }
}
