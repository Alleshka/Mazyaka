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

        int GetAllGameCount();
        int GetWaitGameCount();
        int GetActGameCount();

        /// <summary>
        /// Отправить в игру начальные данные
        /// </summary>
        void SendStartMaze(Guid gameID, Guid userID, MazeArea area = null);
        void SendStartPoint(Guid gameID, Guid userID, Point point = null);



        // TODO : Точно должно быть не тут
        bool MoveObject(Guid gameID, Guid playerID, MoveDirection direction); // Передвинуть объект
        Player GetInitData(Guid gameID, Guid userID); // Получить начальные данные в игре
    }
}
