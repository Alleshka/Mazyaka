using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.General;

namespace MazeProject.GameService
{
    public interface IGameService
    {
        Guid CreateGame(Guid userID);
        Guid JoinGame(Guid userID, Guid gameID);
    }

    public abstract class AbstractGameService : IGameService
    {
        protected IManager<MazeLobby> lobbyManager;

        public abstract Guid CreateGame(Guid userID);

        public abstract Guid JoinGame(Guid userID, Guid gameID);
    }
}
