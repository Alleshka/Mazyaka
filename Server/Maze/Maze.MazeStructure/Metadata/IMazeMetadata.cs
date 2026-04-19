using Maze.MazeStructure.MazeSites;

namespace Maze.MazeStructure.Metadata
{
    public interface IMazeMetadata
    {
        void SetClass(IMazeConnection connection, ConnectionClass cls);
        ConnectionClass GetClass(IMazeConnection connection);

        void MarkExit(IMazeConnection connection);
        bool IsExit(IMazeConnection connection);
        
        bool IsBoundary(IMazeConnection connection);
    }
}
