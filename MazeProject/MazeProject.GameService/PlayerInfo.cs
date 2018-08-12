using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.GameService
{
    public class PlayerInfo
    {
        public Guid UserID { get; private set; }
        public Guid MazeID { get; private set; }

        public Guid ObjectID { get; set; }

        public PlayerInfo(Guid userID, Guid mazeID = default(Guid))
        {
            this.UserID = userID;
            this.MazeID = mazeID;
            
        }

        public void AddMaze(Guid mazeID)
        {
            this.MazeID = mazeID;
        }
    }
}
