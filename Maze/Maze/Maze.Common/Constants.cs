using System;
using System.Collections.Generic;

namespace Maze.Common
{
    public static class Constants
    {
        public static class ServerConstants
        {
            public static readonly Int32 PACKAGE_QUEUE_HANDLER_SLEEP = 20;
        }

        // TODO: https://mazeproject.atlassian.net/browse/MAZE-17
        public static class Roles
        {
            public const String ADMIN = "Admin";
            public const String PLAYER = "Player";
            public const String GUEST = "Guest";
        }

        public static class Loggers
        {
            public static readonly String CommonLogger = "CommonLogger";
        }
    }
}
