using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.MazeService.LoginService
{
    public class LoginResult
    {
        public bool IsSuccess { get; }
        public string SecurityToken { get; }
    }
}
