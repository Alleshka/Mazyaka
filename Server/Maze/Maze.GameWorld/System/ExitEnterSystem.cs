using Maze.GameWorld.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.GameWorld.System
{
    internal class ExitEnterSystem
    {
        public void Process(MazeState world)
        {
            foreach (var e in world.Query<EnteredExitEvent>())
            {
                Console.WriteLine($"Entity {e} won");
                world.Remove<EnteredExitEvent>(e);
            }
        }
    }
}
