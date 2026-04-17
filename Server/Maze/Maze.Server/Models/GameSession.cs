using Maze.MazeStructure;

namespace Maze.Server.Models;

public class GameSession
{
    public Maze.GameWorld.GameWorld World { get; }
    public IMazeInfo MazeInfo { get; }
    public int Rows { get; }
    public int Cols { get; }

    public GameSession(Maze.GameWorld.GameWorld world, IMazeInfo mazeInfo, int rows, int cols)
    {
        World = world;
        MazeInfo = mazeInfo;
        Rows = rows;
        Cols = cols;
    }
}
