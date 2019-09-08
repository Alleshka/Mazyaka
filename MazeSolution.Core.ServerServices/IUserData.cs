using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.ServerServices
{
    public interface IUserData
    {
        Guid Id { get; }
        string Login { get; }
        string Password { get; }
    }
}
