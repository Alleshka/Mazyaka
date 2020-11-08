using Maze.Common.Model;
using Maze.Server.MazeService;
using System;

namespace Maze.Server.Core.Repositories
{
    public interface IUserService : IMazeService
    {
        MazeUser GetUserByID(Guid id);
        MazeUser GetUserByLogin(String userLogin);

        MazeUser CreateUser(MazeUser user);
    }
}
