using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mazyaka.MazeGeneral.MazeModel;
using Mazyaka.MazeGeneral.GameModel;

namespace Mazyaka.Server.GameService
{
    public interface IGameService
    {
        GameRoom CreateGame(Guid userID); // Создать игру
        bool JoinGame(Guid gameID, Guid userID); // Присоединиться к игре
        bool LeaveGame(Guid gameID, Guid userID); // Покинуть игру

        Guid StartGame(Guid gameID);

        // TODO : Точно должно быть не тут

        /// <summary>
        /// Отправить в игру начальные данные
        /// </summary>
        void SendStartMaze(Guid gameID, Guid userID, MazeArea area = null);
        bool SendStartPoint(Guid gameID, Guid userID, Point point = null);

        bool MoveObject(Guid gameID, Guid playerID, MoveDirection direction); // Передвинуть объект
    }
}
