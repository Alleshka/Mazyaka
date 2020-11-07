using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.SessionStorage
{
    public interface ISessionStorage
    {
        string AddUserSession(MazeUser user);

        MazeUser GetUserByLoginOrNull(string userLogin);
        MazeUser GetUserByTokenOrNull(string userToken);

        MazeUserRole GetUserRoleOrNull(string userToken);

        void DeleteSession(string userToken);
    }
}
