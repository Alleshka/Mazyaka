using System;
using Mazyaka.CommonModel.GameModel;

namespace Mazyaka.CommonModel.MazeConnection
{
    interface IMazeConnection
    {
        void Connect(String ip, int port); // Подключиться к серверу

        Guid Login(String login, String password); // Запросить ID

        Guid CreateGame(Guid userID); // Создать комнату
        Guid JoinGame(Guid gameID, Guid userID); // Присоединиться к игре

        void SendMaze(Guid gameID, Guid userID, MazeArea maze); // Отправить лабиринт в игру
        void SendStartPoint(Guid gameID, Guid userID, Point point);

        bool MoveObject(Guid gameID, Guid userID, Guid objecID, MoveDirection direction); // Передвинуть объект 
    }
}