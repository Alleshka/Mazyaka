using MazeProject.General;
using MazeProject.General.Mazes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MazeProject.GameService
{
    public interface IGame
    {
        void AddMaze(MazeArea area);
        bool IsAllHaveMaze();
        void SetStartPoint(Guid playerID, PositionInMaze position);
    }

    public abstract class AbstractGame : IGame
    {
        protected List<PlayerInfo> playerList;
        protected IManager<MazeArea> mazeManager;
        protected int curPlayerIndex;
        protected Random T;

        public AbstractGame(IManager<MazeArea> manager)
        {
            T = new Random();
            playerList = new List<PlayerInfo>();
            this.mazeManager = manager;
        }

        public abstract void AddMaze(MazeArea area);

        public abstract bool IsAllHaveMaze();

        public abstract void SetStartPoint(Guid playerID, PositionInMaze position);
    }

}
