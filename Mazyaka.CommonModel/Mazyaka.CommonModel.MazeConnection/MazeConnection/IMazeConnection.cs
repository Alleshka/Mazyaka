using System;

namespace Mazyaka.CommonModel.MazeConnection
{
    interface IMazeConnection
    {
        void Connect(String ip, int port); // Подключиться к серверу
        Guid Login(String login, String password); // Запросить ID
    }
}