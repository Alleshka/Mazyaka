using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral;
using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral.Command;

namespace MazeProject.Server.GameService
{
    public delegate void GameIsWaitPoint(List<Guid> userList);
    public delegate void GameIsWaitMaze(List<Guid> userList);
    public delegate void GameIsStarted(Guid firstUser);
    public delegate void GameIsFinished(List<Guid> userList);

    public enum StatusGame { WAITPLAYERS, WAITMAZES, WAINPOINTS, STARTED, FINISHED }

    public class GameRoom
    {
        public static Random T = new Random();

        public object locker = new object(); // Локер
        public StatusGame Status { get; set; } // Статус игры

        // TODO: Возможно, стоит ввести тип игры, чтобы было различие в количестве игроков и размере лабиринта
        public readonly int MAZESIZE; // Размер лабиринтов
        public readonly int PLAYERCOUNT; // Количество игроков в комнате

        public Guid RoomID { get; set; } // ID комнаты
        private List<PlayerInfo> PlayerList; // Список игроков в данной комнате
        private List<Maze> MazeList; // Список лабиринтов в данной комнате

        public int PlayerCount => PlayerList.Count();
        public int MazeCount => MazeList.Count();

        public GameRoom()
        {
            RoomID = Guid.NewGuid();
            PlayerList = new List<PlayerInfo>();
            MazeList = new List<Maze>();

            MAZESIZE = 10;
            PLAYERCOUNT = 2;

            Status = StatusGame.WAITPLAYERS;
        }
        public void AddPlayer(Guid playerID)
        {
            PlayerInfo player = new PlayerInfo()
            {
                PlayerID = playerID
            };
            PlayerList.Add(player);
        }

        public void AddMaze(Guid IDCreator, Maze mazeStruct = null)
        {
            Maze temp = null;

            if (mazeStruct != null)
            {
                temp = new Maze(mazeStruct.GetMazeStruct()); // Создаём новый лабиринт

                // Устанавливаем выходы
                temp.SetExit(MoveDirection.UP, mazeStruct.ExitUp.Column);
                temp.SetExit(MoveDirection.RIGHT, mazeStruct.ExitRight.Line);
                temp.SetExit(MoveDirection.LEFT, mazeStruct.ExitLeft.Line);
                temp.SetExit(MoveDirection.DOWN, mazeStruct.ExitDown.Column);
            }
            else
            {
                IMazeGenerator generator = new MazeGeneral.Maze.MazeGenerators.ReqursiveGenerator(T);
                // TODO : Размер лабиринта задавать снаружи
                temp = new Maze(generator, 10); // Добавляем лабиринт

                // Устанавливаем выход
                temp.SetExit(MoveDirection.UP, T.Next(10));
                temp.SetExit(MoveDirection.RIGHT, T.Next(10));
                temp.SetExit(MoveDirection.LEFT, T.Next(10));
                temp.SetExit(MoveDirection.DOWN, T.Next(10));
            }

            MazeList.Add(temp); // Добавляем лабиринт в список лабиринтов

            PlayerInfo player = PlayerList.Where(x => x.PlayerID != IDCreator).Where(x => x.MazeID == Guid.Empty).First(); // Находим пользователя без лабиринта
            player.MazeID = temp.MazeID; // Привязываем лабиринт
        }
        public void AddLiveObject(Guid userID, LiveGameObject @object)
        {
            // Связываем игрока и объект
            PlayerInfo player = PlayerList.Where(x => x.PlayerID == userID).First();
            player.ObjectID = @object.ObjectID;

            // Добавляем объект в лабиринт
            Maze maze = MazeList.Where(x => x.MazeID == player.MazeID).First();
            maze.AddObject(@object, @object.CurAddres);
        }

        public Maze FindMazeByID(Guid id)
        {
            return MazeList.Where(x => x.MazeID == id).First();
        }

        public PlayerInfo FindUserByID(Guid id)
        {
            return PlayerList.Where(x => x.PlayerID == id).First();
        }
        public PlayerInfo FindUserByIndex(int index)
        {
            return PlayerList[index];
        }

        public List<Guid> GetUsersID => PlayerList.Select(x => x.PlayerID).ToList();

        public PlayerInfo GetCurUser => FindUserByIndex(stepByUser);

        private int stepByUser = -1;

        // Решает чей ход будет первым
        private void GetFirstStepUser()
        {
            Random T = new Random();
            stepByUser = T.Next(PlayerCount);
        }

        // Передаёт ход следующему
        public void NextUser()
        {
            stepByUser++;
            if (stepByUser == PLAYERCOUNT) stepByUser = 0;
        }
      
        public bool SetStatusGameToWaitMaze()
        {
            if (PLAYERCOUNT == PlayerCount) // Если игроков достаточно
            {
                lock (locker)
                {
                    if (Status != StatusGame.WAITMAZES)
                    {
                        Status = StatusGame.WAITMAZES;
                        gameIsWaitMazeEvent?.Invoke(GetUsersID);
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Проверяет и ереводит статус игры на ожидание стартовых позиций
        /// </summary>
        public void SetStatusGameToWaitPoin()
        {
            if (PlayerList.Where(x => x.MazeID == Guid.Empty).Count() == 0) // проверяем количество лабиринтов
            {
                lock (locker)
                {
                    if (Status != StatusGame.WAINPOINTS) // Если статус уже сменён - то ничего не делаем
                    {
                        Status = StatusGame.WAINPOINTS; // Меняем статус

                        System.Diagnostics.Trace.WriteLine($"GameRoom.SetStatusGameToWaitPoin - начало");
                        gameIsWaitPointEvent?.Invoke(GetUsersID); // Вызываем событие
                        System.Diagnostics.Trace.WriteLine($"GameRoom.SetStatusGameToWaitPoin - конец");
                    }
                }
            }
        }
        public void SetStatusGameToStarted()
        {
            if (PlayerList.All(x => x.ObjectID != Guid.Empty))
            {
                lock (locker)
                {
                    if (Status == StatusGame.WAINPOINTS)
                    {
                        Status = StatusGame.STARTED; // Меняем статус игры
                        GetFirstStepUser(); // Выбираем первого игрока
                        gameIsStartedEvent?.Invoke(GetCurUser.PlayerID);
                    }
                }
            }
        }
        public void SetStatusGameToFinished()
        {
            lock(locker)
            {
                if(Status == StatusGame.STARTED)
                {
                    Status = StatusGame.FINISHED; // Меняем статус игры
                    gameIsFinishedEvent?.Invoke(GetUsersID); // Срабатывает событие
                }
            }
        }

        // Срабатывает при смене статуса на "Ожидает лабиринт"
        public event GameIsWaitMaze GameIsWaitMazeEvent
        {
            add
            {
                gameIsWaitMazeEvent = value;
            }
            remove
            {
                gameIsWaitMazeEvent -= value;
            }
        }
        // Срабатывает при смене статуса на "Ожидает стартовые точки"
        public event GameIsWaitPoint GameIsWaitPointEvent
        {
            add
            {
                gameIsWaitPointEvent = value;
            }
            remove
            {
                gameIsWaitPointEvent -= value;
            }
        }
        // Срабатывает при старте игры
        public event GameIsStarted GameIsStartedEvent
        {
            add
            {
                gameIsStartedEvent = value;
            }
            remove
            {
                gameIsStartedEvent -= value;
            }
        }
        public event GameIsFinished GameIsFinishedEvent
        {
            add
            {
                gameIsFinishedEvent = value;
            }
            remove
            {
                gameIsFinishedEvent -= value;
            }
        }

        // Срабатывает при смене статуса на "Ожидает лабиринт"
        private event GameIsWaitMaze gameIsWaitMazeEvent;
        // Срабатывает при смене статуса на "Ожидает стартовые точки"
        private event GameIsWaitPoint gameIsWaitPointEvent;
        // Срабатывает при старте игры
        private event GameIsStarted gameIsStartedEvent;
        private event GameIsFinished gameIsFinishedEvent;
    }
}
