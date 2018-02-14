using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazyaka.CommonModel.GameModel;

namespace Mazyaka.Server.GameService
{
    public class TestGameService : IGameService
    {

        private List<GameRoom> waitGameList; // Список ожидающих игр
        private List<GameRoom> actionGameList; // Список активных игр

        private Random T;

        public TestGameService()
        {
            waitGameList = new List<GameRoom>();
            actionGameList = new List<GameRoom>();
            T = new Random();
        }

        public GameRoom CreateGame(Guid userID)
        {
            Player player = new Player(userID); // Создаём игрока

            GameRoom room = new GameRoom(); // Создаём игровую комнату
            room.AddPlayer(player); // Добавляем игрока в комнату

            waitGameList.Add(room); // Добавляем в ожидающие игры
            return room;
        }

        public Player GetStartDate(Guid gameID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public bool JoinGame(Guid gameID, Guid userID)
        {
            Player player = new Player(userID);

            GameRoom room = waitGameList.Where(x => x.RoomID == gameID).First(); // Находим комнату
            room.AddPlayer(player);

            // TODO: в игре должно быть задано количество игроков и тут проверяться
            if (room.PlayerList.Count >= 2)
            {
                actionGameList.Add(room); // Добавляем в список активных
                waitGameList.Remove(room); // Удаляем из ожидающих
            }

            return true; // Сообщаем, что присоединились
        }

        public bool MoveObject(Guid gameID, Guid userID, Guid objectID, MoveDirection direction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// После старта игры добавляет лабиринт в указанную игру
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="area"></param>
        public void SendMaze(Guid gameID, Guid userID, MazeArea area)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First(); // Находим игру
            room.AddMaze(area);

            Player player = room.PlayerList.Where(x => x.PlayerID != userID).First(); // Выбираем игрока, для которого лабиринт
            player.LabirintID = area.MazeID;
        }

        public void SendStartPoint(Guid gameID, Guid userID, Point point)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First(); // Находим игру
            Player player = room.PlayerList.Where(x => x.PlayerID == userID).First(); // Находим игрока
            MazeArea area = room.MazeList.Where(x => x.MazeID == player.LabirintID).First(); // Находим лабиринт

            // Создаём человека
            Human human = new Human()
            {
                Position = point
            };

            area.AddGameObject(human); // Добавляем в лабиринт
            player.ObjectID = human.ID; // Сообщаем игроку что возиь
        }
    }
}
