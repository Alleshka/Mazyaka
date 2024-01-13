namespace Maze.Common
{
    public static class Extensions
    {
        public static MoveDirection Opposite(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.None:
                    {
                        return MoveDirection.None;
                    }
                case MoveDirection.Left:
                    {
                        return MoveDirection.Right;
                    }
                case MoveDirection.Right:
                    {
                        return MoveDirection.Left;
                    }
                case MoveDirection.Up:
                    {
                        return MoveDirection.Down;
                    }
                case MoveDirection.Down:
                    {
                        return MoveDirection.Up;
                    }
            }

            return MoveDirection.None;
        }
    }
}