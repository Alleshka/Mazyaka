using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazyaka.MazeGeneral;
using Mazyaka.MazeGeneral.MazeModel;
using Mazyaka.MazeGeneral.GameModel;

namespace Mazyaka.MazeClientLibrary
{
    interface IMazeClient
    {
        void Connect(String ip, int port); // Подключиться к серверу

        Guid Login(String login, String password); // Запросить ID

        Guid CreateGame(Guid userID); // Создать комнату
        bool JoinGame(Guid gameID, Guid userID); // Присоединиться к игре

        void SendStartMaze(Guid gameID, Guid userID, MazeArea maze); // Отправить лабиринт в игру
        void SendStartPoint(Guid gameID, Guid userID, Point point); // Отправить начальную точку

        Player GetInitData(Guid gameID, Guid userID); // Запросить начальные данные игры
        bool MoveObject(Guid gameID, Guid userID, MoveDirection direction); // Передвинуть объект
    }
}
