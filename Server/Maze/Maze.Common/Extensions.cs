namespace Maze.Common
{
    public static class Extensions
    {
        public static MoveDirection Opposite(this MoveDirection direction)
        {
            MoveDirection result = MoveDirection.None;

            if (direction.HasFlag(MoveDirection.Left))
            {
                result |= MoveDirection.Right;
            }

            if (direction.HasFlag(MoveDirection.Right))
            {
                result |= MoveDirection.Left;
            }

            if (direction.HasFlag(MoveDirection.Up))
            {
                result |= MoveDirection.Down;
            }

            if (direction.HasFlag(MoveDirection.Down))
            {
                result |= MoveDirection.Up;
            }

            return result;
        }
    }
}