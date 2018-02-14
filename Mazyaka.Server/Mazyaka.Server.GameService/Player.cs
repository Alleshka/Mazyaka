using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mazyaka.Server.GameService
{
    /// <summary>
    /// Класс, описывающий игрока
    /// </summary>
    public class Player
    {
        public Guid PlayerID { get; private set; }
        public Guid LabirintID { get; set; } // Лабиринт, в котором пользователь передвигает объект
        public Guid ObjectID { get; set; } // Объект, который пользователь передвигает в лабиринте

        public Player(Guid player)
        {
            PlayerID = player;
        }
    }
}
