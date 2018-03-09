using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazyaka.MazeGeneral.GameModel;
using Mazyaka.MazeGeneral.MazeModel;

namespace Mazyaka.Server.GameService
{
    // Игровая комната
    public class GameRoom
    {
        public Guid RoomID { get; private set; }

        public List<Player> PlayerList { get; private set; } // Список игроков в данной комнате 
        public List<MazeArea> MazeList { get; private set; } // Список лабиринтов в данной комнате

        public Guid curStepUser; // Ход игрока с ID

        public GameRoom()
        {
            RoomID = Guid.NewGuid();

            PlayerList = new List<Player>();
            MazeList = new List<MazeArea>();
        }

        public void AddPlayer(Player player)
        {
            PlayerList.Add(player);
        }

        public void AddMaze(MazeArea maze)
        {
            MazeList.Add(maze);
        }
    }
}