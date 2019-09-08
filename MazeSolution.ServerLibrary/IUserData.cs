using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.ServerLibrary
{
    public interface IUserData
    {
        Guid UserID { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}
