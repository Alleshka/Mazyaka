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
    public enum StatusGame { WAITPLAYERS, WAITMAZES, WAINPOINTS, STARTED }

    public class GameRoom
    {
        public StatusGame Status { get; set; }

        // TODO: Возможно, стоит ввести тип игры, чтобы было различие в количестве игроков и размере лабиринта
        public readonly int MAZESIZE;
        public readonly int PLAYERCOUNT;

        public Guid RoomID { get; set; } // ID комнаты
        public List<PlayerInfo> PlayerList { get; private set; } // Список игроков в данной комнате
        private List<Maze> MazeList { get; set; } // Список лабиринтов в данной комнате

        public int StepByUser;

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

            if(mazeStruct!=null)
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
                IMazeGenerator generator = new MazeGeneral.Maze.MazeGenerators.ReqursiveGenerator();
                // TODO : Размер лабиринта задавать снаружи
                temp = new Maze(generator, 10); // Добавляем лабиринт

                // Устанавливаем выход
                temp.SetExit(MoveDirection.UP, 5);
                temp.SetExit(MoveDirection.RIGHT, 5);
                temp.SetExit(MoveDirection.LEFT, 5);
                temp.SetExit(MoveDirection.DOWN, 5);
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

        public int CountPlayers() => PlayerList.Count();
        public int CountMazes() => MazeList.Count();

        public Maze GetMaze(Guid id) => MazeList.Where(x => x.MazeID == id).First();
        public PlayerInfo GetPlayerByID(Guid id) => PlayerList.Where(x => x.PlayerID == id).First();
        public PlayerInfo GetPlayerByIndex(int index) => PlayerList[index];

        public void StartGame()
        {
            Random T = new Random();
            StepByUser = T.Next(PLAYERCOUNT); // Решаем, кто первый ходит
        }

        public void NextUser()
        {
            StepByUser++;
            if (StepByUser == PLAYERCOUNT) StepByUser = 0;
        }
    }
}
