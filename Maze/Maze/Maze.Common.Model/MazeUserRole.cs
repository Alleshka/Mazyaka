using System;
using System.ComponentModel;

namespace Maze.Common.Model
{
    [Flags]
    public enum MazeUserRole
    {
        [Description(Constants.Roles.ADMIN)]
        Admin = 1 << 0,

        [Description(Constants.Roles.PLAYER)]
        Player = 1 << 1,

        [Description(Constants.Roles.GUEST)]
        Guest = 1 << 2,

        All = Admin | Player | Guest,
        NotGuest = Admin | Player
    }
}
