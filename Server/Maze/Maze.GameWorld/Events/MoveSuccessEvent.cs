using Maze.GameWorld.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.GameWorld.Evemts
{
    public record struct MoveSuccessEvent(RoomPostition Postition);
    public struct MoveExitEvent { }
}
