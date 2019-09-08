using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.ServerLibrary
{
    public interface IGameService
    {
        Guid CreateGame(Guid userID);
        bool JoinGame(Guid gameID, Guid userID);

    }
}
