using Maze.Common.Model;
using Maze.Server.MazeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Repositories
{
    public interface IUserService : IMazeService
    {
        MazeUser GetUserByID(Guid id);
        MazeUser GetUserByLogin(string userLogin);

        MazeUser CreateUser(MazeUser user);
    }
}
