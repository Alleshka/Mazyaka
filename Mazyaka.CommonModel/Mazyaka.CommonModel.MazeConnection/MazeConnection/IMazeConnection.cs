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

        bool GameIsStart(Guid idGame); // Проверить началась ли игра
        void SendMaze(Guid gameID, Guid userID, MazeArea maze); // Отправить лабиринт в игру
        bool IsMyStep(Guid gameID, Guid userID); // Проверить мой ли ход

        bool MoveObject(Guid gameID, Guid userID, Guid objecID); // Передвинуть объект 

        // TODO: Можно передават олько игру и пользователя, так как id объекта лежит внутри
        // TODO: С другой стороны можно оставить, так как пользователь может двигать разные объекты

        bool WaitMyStep(Guid gameID, Guid userID); // Отправить команду на ожидание своего хода
        bool WaitStartGame(Guid gameID); // Отправить команду на ожидание старта игры
    }
}