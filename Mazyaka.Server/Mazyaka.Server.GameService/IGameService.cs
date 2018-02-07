using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazyaka.Server.GameService
{
    public interface IGameService
    {
        GameRoom CreateGame(Guid userID);
        bool JoinGame(Guid gameID, Guid userID);

        bool CheckIsGameStart(Guid gameID);
        bool CheckIsMyStep(Guid gameID, Guid userID);
    }
}
