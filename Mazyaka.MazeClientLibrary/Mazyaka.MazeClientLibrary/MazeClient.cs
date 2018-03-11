using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mazyaka.MazeGeneral;
using Mazyaka.MazeGeneral.GameModel;
using Mazyaka.MazeGeneral.MazeModel;

namespace Mazyaka.MazeClientLibrary
{
    public class MazeClient : IMazeClient
    {
        private Connection connection = new Connection();

        public void Connect(string ip, int port)
        {
            connection.ConnectServer(ip, port);
        }

        public Guid CreateGame(Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.CreateGame);
            command.AddArgument(userID.ToString());

            var responce = connection.SendCommand(command);
            Guid.TryParse(responce[0], out Guid gameID);

            return gameID;
        }

        public Player GetInitData(Guid gameID, Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.GetInitData);
            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());

            var responce = connection.SendCommand(command);
            return Player.ToObject(responce[0]);
        }

        public bool JoinGame(Guid gameID, Guid userID)
        {
            PackCommand command = new PackCommand(TypeCommand.JoinGame);
            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());

            var resp = connection.SendCommand(command);
            return Convert.ToBoolean(resp[0]);
        }

        public Guid Login(string login, string password)
        {
            PackCommand command = new PackCommand(TypeCommand.Login);
            command.AddArgument(login);
            command.AddArgument(password);

            var resp = connection.SendCommand(command);
            Guid.TryParse(resp[0], out Guid userID);
            return userID;
        }

        public bool MoveObject(Guid gameID, Guid userID, MoveDirection direction)
        {
            throw new NotImplementedException();
        }

        public void SendStartMaze(Guid gameID, Guid userID, MazeArea maze)
        {
            PackCommand command = new PackCommand(TypeCommand.SendStartMaze);
            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());
            command.AddArgument(MazeArea.ToXML(maze));

            var resp = connection.SendCommand(command);
        }

        public void SendStartPoint(Guid gameID, Guid userID, Point point)
        {
            PackCommand command = new PackCommand(TypeCommand.SendStartPoint);
            command.AddArgument(gameID.ToString());
            command.AddArgument(userID.ToString());
            command.AddArgument(Point.ToXML(point));

            var resp = connection.SendCommand(command);
        }
    }
}
