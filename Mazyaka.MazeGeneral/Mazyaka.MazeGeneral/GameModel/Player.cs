using System;

namespace Mazyaka.MazeGeneral.GameModel
{
    /// <summary>
    /// Класс, описывающий игрока
    /// </summary>
    public class Player : TransmittedClass<Player>
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
