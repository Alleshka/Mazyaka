using Maze.Common.Model;
using Maze.Server.MazeService;

namespace Maze.Server.Core.SessionStorage
{
    public interface ISessionStorage : IMazeService
    {
        string AddUserSession(MazeUser user);

        MazeUser GetUserByTokenOrNull(string userToken);

        MazeUserRole GetUserRoleOrNull(string userToken);

        void DeleteSession(string userToken);
    }
}
