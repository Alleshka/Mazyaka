using Maze.MazeStructure.MazeSites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.MazeStructure.Metadata
{
    public class MazeMetadata : IMazeMetadata
    {
        private Dictionary<IMazeConnection, ConnectionDirectionTag> _directionTags = new Dictionary<IMazeConnection, ConnectionDirectionTag>();
        private Dictionary<IMazeConnection, ConnectionTypeTag> _typeTags = new Dictionary<IMazeConnection, ConnectionTypeTag>();

        public void AddDirectionTag(IMazeConnection connection, ConnectionDirectionTag tag)
        {
            throw new NotImplementedException();
        }

        public void AddTypeTag(IMazeConnection connection, ConnectionTypeTag tag)
        {
           if (_typeTags.TryGetValue(connection, out var existingTag))
            {
                _typeTags[connection] = existingTag | tag;
            }
            else
            {
                _typeTags[connection] = tag;
            }
        }

        public bool HasDirectionTag(IMazeConnection connection, ConnectionDirectionTag tag)
        {
            throw new NotImplementedException();
        }

        public bool HasTypeTag(IMazeConnection connection, ConnectionTypeTag tag)
        {
            return _typeTags.TryGetValue(connection, out var existingTag) && (existingTag & tag) != 0;
        }

        public void RemoveDirectionTag(IMazeConnection connection, ConnectionDirectionTag tag)
        {
            throw new NotImplementedException();
        }

        public void RemoveTypeTag(IMazeConnection connection, ConnectionTypeTag tag)
        {
            if (_typeTags.TryGetValue(connection, out var existingTag))
            {
                _typeTags[connection] = existingTag & ~tag;
            }
        }
    }
}
