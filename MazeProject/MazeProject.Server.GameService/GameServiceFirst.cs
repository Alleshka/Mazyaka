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
        public object locker = new object();
        
        private List<GameRoom> waitGameList;
        private List<GameRoom> actionGameList;

        public GameServiceFirst()
        {
            waitGameList = new List<GameRoom>(10);
            actionGameList = new List<GameRoom>(10);
        }

        public Guid CreateGame(Guid userID)
        {
            GameRoom room = new GameRoom();
            room.AddPlayer(userID);

            waitGameList.Add(room);
            return room.RoomID;
        }
        public bool JoinGame(Guid userID, Guid gameID)
        {
            // TODO: Тут могут быть косяки из-за конкуренции
            GameRoom room = waitGameList.Where(x => x.RoomID == gameID).First();

            room.AddPlayer(userID);
            if (room.SetStatusGameToWaitMaze())
            {
                actionGameList.Add(room);
                waitGameList.Remove(room);
            }
            return true;
        }

        public void AddMaze(Guid gameID, Guid userID, Maze maze = null)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();

            room.AddMaze(userID, maze); // TODO : Как ввести возможность добавления в один лабиринт(???)
            room.SetStatusGameToWaitPoin();
        }

        public void AddLive(Guid gameID, Guid userID, MazePoint point = null)
        {
            if (point == null) point = new MazePoint(5, 5);

            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();

            LiveGameObject human = new Human(point); // TODO: Тут в зависимсти от разных типов игры должны создаваться разные игроки 
            room.AddLiveObject(userID, human); // Связываем игрока и его объект

            room.SetStatusGameToStarted(); // Запускаем игру
        }

        public bool? MoveObject(Guid gameID, Guid userID, MoveDirection direction)
        {
            // TODO : Добавить проверку на пользователя
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First(); // Находим комнату
            PlayerInfo user = room.FindUserByID(userID); // Достаём инфу об игроке

            Maze maze = room.FindMazeByID(user.MazeID); // Достаём лабиринт
            bool? moved = maze.MoveObject(user.ObjectID, direction); // Двигаем объект

            if (moved == null)
            {
                room.SetStatusGameToFinished(); // Завершаем игру
                return moved;
            }
            else
            {
                room.NextUser(); // Меняем ход
                return moved;
            }
        }


        public GameRoom FindGameByID(Guid gameID)
        {
            return (waitGameList.Concat(actionGameList).Where(x => x.RoomID == gameID).First());
        }

        public List<Guid> GamesList()
        {
            return waitGameList.Select(x => x.RoomID).ToList();
        }

        public Guid CurUser(Guid gameID)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            return room.GetCurUser.PlayerID;
        }
    }
}
