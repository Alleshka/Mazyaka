using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral;

namespace MazeProject.Server.GameService
{

    public delegate void HasUserConnected(Guid id, bool status);
    public delegate void GameIsWaitMaize(List<Guid> userList);

    public interface IGameService
    {
        Guid CreateGame(Guid userID);
        bool JoinGame(Guid userID, Guid gameID);

        GameRoom FindGameByID(Guid gameID);

        List<Guid> GamesList();

        void AddMaze(Guid gameID, Guid userID, Maze maze = null);
        void AddLive(Guid gameID, Guid userID, MazePoint point = null);

        bool MoveObject(Guid gameID, Guid userID, MoveDirection direction);

        Guid StepByUser(Guid gameID);

        /// <summary>
        /// Срабатывает при неудачном подключени к комнате
        /// </summary>
        event HasUserConnected HasUserConnectedEvent;

        event GameIsWaitMaize GameIsWaitMazeEvent;
    }
}
