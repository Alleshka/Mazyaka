using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazyaka.Server.GameService
{
    public class TestGameService : IGameService
    {

        private List<GameRoom> waitGameList; // Список ожидающих игр
        private List<GameRoom> actionGameList; // Список активных игр

        public TestGameService()
        {
            waitGameList = new List<GameRoom>();
            actionGameList = new List<GameRoom>();
        }

        public GameRoom CreateGame(Guid userID)
        {
            Player player = new Player(userID); // Создаём игрока

            GameRoom room = new GameRoom(); // Создаём игровую комнату
            room.AddPlayer(player); // Добавляем игрока в комнату

            waitGameList.Add(room); // Добавляем в ожидающие игры
            return room;
        }

        public bool JoinGame(Guid gameID, Guid userID)
        {
            Player player = new Player(userID);

            GameRoom room = waitGameList.Where(x => x.RoomID == gameID).First(); // Находим комнату
            room.AddPlayer(player);

            // TODO: в игре должно быть задано количество игроков и тут проверяться
            actionGameList.Add(room); // Добавляем в список активных
            waitGameList.Remove(room); // Удаляем из ожидающих

            return true; // Сообщаем, что присоединились
        }
    }
}
