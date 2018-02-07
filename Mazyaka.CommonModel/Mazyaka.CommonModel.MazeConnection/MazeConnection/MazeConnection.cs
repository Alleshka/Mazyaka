using System;
using System.Linq;
using Mazyaka.CommonModel.GameModel;

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
            PackCommand command = new PackCommand(TypeCommand.Login); // Создаём команду 

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
            PackCommand command = new PackCommand(TypeCommand.CreateGame); // Создаём команду

            // Добавляем ID пользователя в параметры
            command.AddArgument(userID.ToString());
            command = connection.SendCommand(command); // Отправляем команду

            Guid.TryParse(command.args[0], out Guid id); // Тут лежит ID созданной комнаты

            return id; // Возвращаем id игры
        }

        public Guid JoinGame(Guid gameID, Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.JoinGame);

            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());

            Guid.TryParse(command.args[0], out Guid id);
            return id;
        }

        /// <summary>
        /// Проверяет началась ли игра
        /// </summary>
        /// <param name="idGame"></param>
        /// <returns>true - если игра началась, false - если набор ещё идёт</returns>
        public bool GameIsStart(Guid idGame)
        {
            PackCommand command = new PackCommand(TypeCommand.IsGameStart);
            command.AddArgument(idGame.ToString());

            command = connection.SendCommand(command);

            bool status = Convert.ToBoolean(command[0]);
            return status;
        }
        
        public void SendMaze(Guid gameID, Guid userID, MazeArea maze)
        {
            PackCommand command = new PackCommand(TypeCommand.SendMaze);

            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());
            command.AddArgument(MazeArea.ToJson(maze));

            connection.SendMessage(command);
        }

        public bool IsMyStep(Guid gameID, Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.IsMyStep);

            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());

            command = connection.SendCommand(command);

            bool step = Convert.ToBoolean(command[0]);
            return step;
        }

        public bool MoveObject(Guid gameID, Guid userID, Guid objecID)
        {
            PackCommand command = new PackCommand(TypeCommand.MoveObject);

            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());
            command.AddArgument(objecID.ToString());

            command = connection.SendCommand(command);

            bool status = Convert.ToBoolean(command[0]);
            return status;
        }
    }
}