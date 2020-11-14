using Maze.Common.Model;
using System;

namespace Maze.Server.MazeService.LoginService
{
    public interface ILoginService : IMazeService
    {
        MazeUser GetUserByID(Guid id);
        MazeUser GetUserByLogin(String userLogin);

        MazeUser CreateUser(MazeUser user);
    }
}
