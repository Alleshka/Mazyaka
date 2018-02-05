using System;

namespace Mazyaka.CommonModel.MazeConnection
{
    public class MazeConnection : IMazeConnection
    {
        private ConnectionLib connection = new ConnectionLib();

        public void Connect(string ip, int port)
        {
            connection.ConnectServer(ip, port); // Подключаемся к серверу
        }

        /// <summary>
        /// Запросить ID у сервера
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Guid Login(String login, String password)
        {
            PackCommand command = new PackCommand(TypeCommand.login); // Создаём команду 

            // Добавляем параметры
            command.AddArgument(login);
            command.AddArgument(password);

            command = connection.SendCommand(command); // Отправляем запрос серверу

            // [id] - 
            Guid.TryParse(command.args[0], out Guid id); // ID лежит в первом аргументе

            return id; // Возвращаем ID
        }

        public Guid CreateGame(Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.creategame); // Создаём команду

            // Добавляем ID пользователя в параметры
            command.AddArgument(userID.ToString());
            command = connection.SendCommand(command); // Отправляем команду

            Guid.TryParse(command.args[0], out Guid id); // Тут лежит ID созданной комнаты

            return id; // Возвращаем id игры
        }

        public Guid JoinGame(Guid gameID, Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.joingame);

            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());

            Guid.TryParse(command.args[0], out Guid id);
            return id;
        }
    }
}