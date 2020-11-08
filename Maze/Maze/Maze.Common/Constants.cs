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
            public static readonly String ADMIN = "Admin";
            public static readonly String PLAYER = "Player";
            public static readonly String GUEST = "Guest";

            public static readonly IEnumerable<String> ALL = new string[] { Roles.ADMIN, Roles.PLAYER, Roles.GUEST };
            public static readonly IEnumerable<String> NOT_GUEST = new string[] { Roles.ADMIN, Roles.PLAYER };
        }
    }
}
