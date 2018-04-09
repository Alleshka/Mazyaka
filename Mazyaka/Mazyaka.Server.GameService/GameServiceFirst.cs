﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazyaka.MazeGeneral.GameModel;
using Mazyaka.MazeGeneral.MazeModel;
using Mazyaka.MazeGeneral.MazeGenerator;

namespace Mazyaka.Server.GameService
{
    /// <summary>
    /// Игровой сервис для обычного режима
    /// </summary>
    public class GameServiceFirst : IGameService
    {
        private Random T;
        private List<GameRoom> waitGameList;
        private List<GameRoom> actionGameList;

        public GameServiceFirst()
        {
            T = new Random();
            waitGameList = new List<GameRoom>();
            actionGameList = new List<GameRoom>();
        }

        public GameRoom CreateGame(Guid userID)
        {
            GameRoom room = new GameRoom(); // Создаём игровую комнату
            room.AddPlayer(new Player(userID)); // Добавляем игрока

            waitGameList.Add(room); // Добавляем в список ожидающих

            return room;
        }

        public bool JoinGame(Guid gameID, Guid userID)
        {
            GameRoom room = waitGameList.Where(x => x.RoomID == gameID).First();
            waitGameList.Remove(room);

            room.AddPlayer(new Player(userID));
            actionGameList.Add(room); // TODO: В других режимах необходимо проверять количество игроков


            return true;
        }

        public void SendStartMaze(Guid gameID, Guid userID, MazeArea area = null)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            Player player = room.PlayerList.Where(x => x.PlayerID != userID).First(); // TODO: В других режимах может быть несколько игроков

            if (area == null)
            {
                RecursiveGenerator generator = new MazeGeneral.MazeGenerator.RecursiveGenerator();
                area = new MazeArea();

                area.SetStructLab(generator.Generate(10));
                area.SetExit(MoveDirection.UP, 5);
                area.SetExit(MoveDirection.RIGHT, 5);
                area.SetExit(MoveDirection.LEFT, 5);
                area.SetExit(MoveDirection.DONW, 5);
            }

            player.LabirintID = area.MazeID;
            room.AddMaze(area);  
        }

        public bool SendStartPoint(Guid gameID, Guid userID, Point point = null)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            Player player = room.PlayerList.Where(x => x.PlayerID == userID).First();
            MazeArea area = room.MazeList.Where(x => x.MazeID == player.LabirintID).First(); // Находим лабиринт для данного игрока

            if (point == null)
            {
                point = new Point(5, 5);
            }

            Human human = new Human()
            {
                Position = point
            };
            player.ObjectID = human.ID;

            area.AddGameObject(human);
            player.IsReady = true;

            // TODO : Уберём, если сделаем по кнопке
            if (room.PlayerList.All(x => x.IsReady == true)) return true;
            else return false;
        }

        public bool MoveObject(Guid gameID, Guid playerID, MoveDirection direction)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            Player player = room.PlayerList.Where(x => x.PlayerID == x.LabirintID).First();

            MazeArea area = room.MazeList.Where(x => x.MazeID == player.LabirintID).First();
            return area.MoveLiveObject(player.ObjectID, direction); // TODO: Решить, нужна ли возможность движения разных объектов
        }

        public bool LeaveGame(Guid gameID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public Guid StartGame(Guid gameID)
        {
            GameRoom room = actionGameList.Where(x => x.RoomID == gameID).First();
            room.curStepUser = room.PlayerList[T.Next(room.PlayerList.Count - 1)].PlayerID;
            return room.curStepUser;
        }
    }
}