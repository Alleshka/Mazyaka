using Maze.Core.Common;
using Maze.MazeStructure.Connections;
using Maze.MazeStructure.Interfaces;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Rooms;
using Maze.MazeStructure.Walls;

namespace Maze.MazeStructure.Builder
{
    public class SimpleMazeBuilder : IMazeBuilder
    {
        private IMaze _currentMaze;

        public void BuildExit(int row, int col)
        {
            var room = _currentMaze.GetRoomByCoordinates(row, col);
            var direction = MoveDirection.None;

            int dLine = 0;
            int dCol = 0;

            if (row == 0)
            {
                direction = MoveDirection.Up;
                dLine -= 1;
            }
            else if (row == _currentMaze.RowCount - 1)
            {
                direction = MoveDirection.Down;
                dLine += 1;
            }
            else if (col == 0)
            {
                direction = MoveDirection.Left;
                dCol -= 1;
            }
            else if (col == _currentMaze.ColCount - 1)
            {
                direction = MoveDirection.Right;
                dCol += 1;
            }

            if (direction != MoveDirection.None)
            {
                room.AddOrUpdateMazeConnection(direction, new SimpleExit());
            }
        }

        public virtual void BuildEmptyMaze(int rowCount, int colCount)
        {
            _currentMaze = new SimpleMaze(rowCount, colCount);
        }

        public virtual void BuildPassage(int line1, int col1, int line2, int col2)
        {
            var room1 = _currentMaze.GetRoomByCoordinates(line1, col1);
            var room2 = _currentMaze.GetRoomByCoordinates(line2, col2);

            BuildPassage(room1, room2);
        }

        public void BuildPassage(IMazeRoom room1, IMazeRoom room2)
        {
            if (room1 != null && room2 != null)
            {
                var passage = new SimpleMazePassage(room1, room2);

                room1.AddOrUpdateMazeConnection(CommonWall(room1, room2), passage);
                room2.AddOrUpdateMazeConnection(CommonWall(room2, room1), passage);
            }
        }

        public virtual void BuildRoom(int line, int col)
        {
            var room = _currentMaze.GetRoomByCoordinates(line, col);
            if (room == null)
            {
                room = new SimpleMazeRoom(line, col);
                _currentMaze.AddRoom(room);
            }
        }

        public virtual void BuildWall(IMazeRoom room1, IMazeRoom room2)
        {
            if (room1 != null && room2 != null)
            {
                var wall = new DestroyableWall(room1, room2);

                // Build walls
                room1.AddOrUpdateMazeConnection(CommonWall(room1, room2), wall);
                room2.AddOrUpdateMazeConnection(CommonWall(room2, room1), wall);
            }
        }

        public void BuildWall(int row1, int col1, int row2, int col2)
        {
            var room1 = _currentMaze.GetRoomByCoordinates(row1, col1);
            var room2 = _currentMaze.GetRoomByCoordinates(row2, col2);

            BuildWall(room1, room2);
        }

        public void BuildNonDestroyableWall(IMazeRoom room,MoveDirection direction)
        {
            if (room != null)
            {
                var wall = new NonDestroyableWall(); // Should we create only one to safe memory?
                room.AddOrUpdateMazeConnection(direction, wall);
            }
        }

        public void BuildNonDestroyableWall(int row, int col, MoveDirection direction)
        {
            var room1 = _currentMaze.GetRoomByCoordinates(row, col);
            BuildNonDestroyableWall(room1, direction);
        }

        public IMaze GetMaze()
        {
            return _currentMaze;
        }

        protected virtual MoveDirection CommonWall(IMazeRoom room1, IMazeRoom room2)
        {
            MoveDirection moveDirection = MoveDirection.None;

            if (room1.Point.Row == room2.Point.Row)
            {
                if (room1.Point.Column < room2.Point.Column)
                {
                    moveDirection = MoveDirection.Right;
                }
                else if (room1.Point.Column > room2.Point.Column)
                {
                    moveDirection = MoveDirection.Left;
                }
            }
            else if (room1.Point.Column == room2.Point.Column)
            {
                if (room1.Point.Row < room2.Point.Row)
                {
                    moveDirection = MoveDirection.Down;
                }
                else if (room1.Point.Row > room2.Point.Row)
                {
                    moveDirection = MoveDirection.Up;
                }
            }

            return moveDirection;
        }

        public void BuildPassage(MazePoint point1, MazePoint point2)
        {
            var room1 = _currentMaze.GetRoomByPoint(point1);
            var room2 = _currentMaze.GetRoomByPoint(point2);

            BuildPassage(room1, room2);
        }

        public void BuildRoom(MazePoint point)
        {
            BuildRoom(point.Row, point.Column);
        }

        public void BuildExit(MazePoint point)
        {
            BuildExit(point.Row, point.Column);
        }
    }
}
