using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral;
using MazeProject.MazeGeneral.Maze;

namespace MazeProject.Server.GameService
{
    public class GameServiceFirst : IGameService
    {
        private List<GameRoom> waitGameList;
        private List<GameRoom> actionGameList;

        public GameServiceFirst()
        {
            waitGameList = new List<GameRoom>(10);
            actionGameList = new List<GameRoom>(10);
        }

        public event HasUserConnected HasUserConnectedEvent;
        public event GameIsWaitMaize GameIsWaitMazeEvent;

        public void AddLive(Guid gameID, Guid userID, MazePoint point = null)
        {
            if (point == null) point = new MazePoint(5, 5);
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();

            LiveGameObject human = new Human(point); // TODO: Тут в зависимсти от разных типов игры должны создаваться разные игроки 
            room.AddLiveObject(userID, human); // Связываем игрока и его объект

            if(room.PlayerList.All(x=>x.ObjectID!=Guid.Empty))
            {
                room.Status = StatusGame.STARTED;
                room.StartGame();
            }
        }

        public void AddMaze(Guid gameID, Guid userID, Maze maze = null)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            room.AddMaze(userID, maze); // TODO : Как ввести возможность добавления в один лабиринт(???)

            // TODO : Пока режим один, проверяем на количество игроков, потом надо проверять
            if(room.PlayerList.All(x=>x.MazeID!=Guid.Empty))
            {
                room.Status = StatusGame.WAINPOINTS;
            }
        }

        public Guid CreateGame(Guid userID)
        {
            GameRoom room = new GameRoom();
            room.AddPlayer(userID);

            waitGameList.Add(room);
            return room.RoomID;
        }

        public GameRoom FindGameByID(Guid gameID)
        {
            return (waitGameList.Concat(actionGameList).Where(x => x.RoomID == gameID).First());
        }

        public List<Guid> GamesList()
        {
            return waitGameList.Select(x => x.RoomID).ToList();
        }

        public bool JoinGame(Guid userID, Guid gameID)
        {
            // TODO: Тут могут быть косяки из-за конкуренции
            GameRoom room = waitGameList.Where(x => x.RoomID == gameID).First();
            room.AddPlayer(userID);

            if (room.PLAYERCOUNT == room.CountPlayers())
            {
                waitGameList.Remove(room);
                actionGameList.Add(room);

                room.Status = StatusGame.WAITMAZES;

                HasUserConnectedEvent?.Invoke(userID, true);
                GameIsWaitMazeEvent?.Invoke(room.UsersIDList());
                return true;
            }
            else
            {
                HasUserConnectedEvent?.Invoke(userID, false);
                return false;
            }
        }

        public bool MoveObject(Guid gameID, Guid userID, MoveDirection direction)
        {
            // TODO : Добавить проверку на пользователя
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First(); // Находим комнату
            PlayerInfo user = room.GetPlayerByID(userID); // Достаём инфу об игроке
            Maze maze = room.GetMaze(user.MazeID); // Достаём лабиринт
            bool moved = maze.MoveObject(user.ObjectID, direction); // Двигаем объект
            room.NextUser(); // Меняем ход
            return moved;
        }

        public Guid StepByUser(Guid gameID)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            return room.GetPlayerByIndex(room.StepByUser).PlayerID;
        }
    }
}
