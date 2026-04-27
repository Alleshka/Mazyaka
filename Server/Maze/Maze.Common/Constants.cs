namespace Maze.Common
{
    public static class Constants
    {
        public static class ConnectionNames
        {
            public const string Wall = "Wall";
            public const string Exit = "Exit";
            public const string Boundary = "Boundary";
            public const string DestroyedWall = "DestroyedWall";
        }

        public static class HubMethods
        {
            public const string CreateGame = "CreateGame";
            public const string SetUser = "SetUser";
            public const string Move = "Move";
            public const string DestroyWall = "DestroyWall";
        }
    }
}
