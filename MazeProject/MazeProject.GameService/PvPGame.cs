using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.General;
using MazeProject.General.Mazes;

namespace MazeProject.GameService
{
    public class PvPGame : AbstractGame
    {
        private int curPlayer;

        public PvPGame(MazeLobby lobby, IManager<MazeArea> manager) : base(manager)
        {
            foreach(var player in lobby.PlayerList)
            {
                playerList.Add(new PlayerInfo(player.PlayerID));
            }
        }

        public override void AddMaze(MazeArea area)
        {
            var player = playerList.Find(x => (x.UserID != area.Creator) && !Guid.Equals(x.MazeID, Guid.Empty));
            player.AddMaze(area.MazeID);

            mazeManager.Add(area);
        }

        public override bool IsAllHaveMaze()
        {
            return !playerList.Any(x => x.MazeID == Guid.Empty);
        }

        public override void SetStartPoint(Guid playerID, PositionInMaze position)
        {
            var player = playerList.Find(x => x.UserID == playerID);
            var maze = mazeManager[player.MazeID];
            var character = new MazeProject.General.GameObjects.Character();

            maze.AddHero(character, position);

            player.ObjectID = character.ObjectID;
        }

        public bool IsAllHaveObject()
        {
            return !playerList.Any(x => x.ObjectID == Guid.Empty);
        }

        public Guid StartGame()
        {
            this.curPlayer = T.Next(0, this.playerList.Count);
            return this.playerList[this.curPlayer].UserID;
            // TODO: Game started event?
        }

        public bool Step(Guid playerID, MoveDirection direction)
        {
            if (playerID == playerList[curPlayer].UserID)
            {
                var player = playerList.Find(x => x.UserID == playerID);
                bool isRemoved = mazeManager[player.MazeID].ReplaceObject(player.ObjectID, direction);
                this.curPlayer = (this.curPlayer + 1) % playerList.Count;
                return isRemoved;
            }
            else
            {
                return false;
            }
        }
    }
}
