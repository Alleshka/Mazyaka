using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazyaka.CommonModel.GameModel;

namespace Mazyaka.Server.GameService
{
    public interface IGameService
    {
        GameRoom CreateGame(Guid userID); // Создать игру 
        bool JoinGame(Guid gameID, Guid userID); // Присоединиться к игре

        void SendMaze(Guid gameID, Guid userID, MazeArea area); // Отправить структуру лабиринта в игру
        void SendStartPoint(Guid gameID, Guid userID, Point point); // Отправить начальные координаты

        Player GameStartData(Guid gameID, Guid userID); // Получить начальные данные

        bool MoveObject(Guid gameID, Guid userID, Guid objectID, MoveDirection direction);
    }
}
