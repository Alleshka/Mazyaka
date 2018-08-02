using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.GameService
{
    public interface IGameService
    {
        Guid CreateGame(Guid userID);
        Guid JoinGame(Guid userID, Guid gameID);
    }
}
