using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Maze;

namespace MazeProject.MazeGeneral
{
    public class PlayerInfo
    {
        public Guid PlayerID { get; set; } = Guid.Empty; // ID игрока
        public Guid MazeID { get; set; } = Guid.Empty; // ID лабиринта, в котором игрок передвигается
        public Guid ObjectID { get; set; } = Guid.Empty; // ID объекта, который передвигается
    }
}
