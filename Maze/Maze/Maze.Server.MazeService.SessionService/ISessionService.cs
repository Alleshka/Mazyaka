using Maze.Common.Model;

namespace Maze.Server.MazeService.SessionService
{
    public interface ISessionService : IMazeService
    {
        string AddUserSession(MazeUser user);

        MazeUser GetUserByTokenOrNull(string userToken);

        MazeUserRole GetUserRoleOrDefault(string userToken);

        void DeleteSession(string userToken);
    }
}
