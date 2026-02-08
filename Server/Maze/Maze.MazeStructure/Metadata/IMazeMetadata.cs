using Maze.MazeStructure.MazeSites;

namespace Maze.MazeStructure.Metadata
{
    public interface IMazeMetadata
    {
        void AddDirectionTag(IMazeConnection connection, ConnectionDirectionTag tag);
        void RemoveDirectionTag(IMazeConnection connection, ConnectionDirectionTag tag);
        bool HasDirectionTag(IMazeConnection connection, ConnectionDirectionTag tag);

        void AddTypeTag(IMazeConnection connection, ConnectionTypeTag tag);
        void RemoveTypeTag(IMazeConnection connection, ConnectionTypeTag tag);
        bool HasTypeTag(IMazeConnection connection, ConnectionTypeTag tag);
    }
}
