using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Repositories
{
    public interface IUserRepository
    {
        MazeUser GetUserByID(Guid id);
        MazeUser GetUserByLogin(string userLogin);

        MazeUser CreateUser(MazeUser user);
    }
}
