using Maze.Common;

namespace Maze.MazeStructure.Builder
{
    public class SimpleMazeBuilder : IMazeBuilder
    {
        private IMaze _currentMaze;

        public void BuildExit(int line, int col)
        {
            var room = _currentMaze.GetRoomByCoordinates(line, col);
            var direction = MoveDirection.None;

            int dLine = 0;
            int dCol = 0;

            if (line == 0)
            {
                direction = MoveDirection.Up;
                dLine -= 1;
            }
            else if (line == _currentMaze.LineCount - 1)
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
                room.SetMazeSite(direction, new SimpleExit(line + dLine, col + dCol));
            }
        }

        public virtual void BuildMaze()
        {
            _currentMaze = new SimpleMaze();
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
                room1.SetMazeSite(CommonWall(room1, room2), room2);
                room2.SetMazeSite(CommonWall(room2, room1), room1);
            }
        }

        public virtual void BuildRoom(int line, int col)
        {
            var room = _currentMaze.GetRoomByCoordinates(line, col);
            if (room == null)
            {
                room = new SimpleMazeRoom(line, col);
                _currentMaze.AddRoom(room);

                room.SetMazeSite(MoveDirection.Left, new SimpleMazeWall());
                room.SetMazeSite(MoveDirection.Right, new SimpleMazeWall());
                room.SetMazeSite(MoveDirection.Up, new SimpleMazeWall());
                room.SetMazeSite(MoveDirection.Down, new SimpleMazeWall());
            }
        }

        public IMaze GetMaze()
        {
            return _currentMaze;
        }

        protected virtual MoveDirection CommonWall(IMazeRoom room1, IMazeRoom room2)
        {
            MoveDirection moveDirection = MoveDirection.None;

            if (room1.Line == room2.Line)
            {
                if (room1.Column < room2.Column)
                {
                    moveDirection = MoveDirection.Right;
                }
                else if (room1.Column > room2.Column)
                {
                    moveDirection = MoveDirection.Left;
                }
            }
            else if (room1.Column == room2.Column)
            {
                if (room1.Line < room2.Line)
                {
                    moveDirection = MoveDirection.Down;
                }
                else if (room1.Line > room2.Line)
                {
                    moveDirection = MoveDirection.Up;
                }
            }

            return moveDirection;
        }
    }
}
