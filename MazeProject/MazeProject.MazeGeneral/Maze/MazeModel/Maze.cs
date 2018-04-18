using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    /// <summary>
    /// Сам лабиринт
    /// </summary>
    [DataContract]
    public class Maze
    {
        [DataMember]
        public Guid MazeID { get; private set; }

        [DataMember]
        private MazeStruct mazeStruct; // Структура лабиринта
        [DataMember]
        private List<LiveGameObject> liveObjects; // Тут лежат все живые объекты
        [DataMember]
        private Cell exitCell; // Тут лежит ячейка с выходом

        public Maze(MazeStruct maze)
        {
            InitMaze();
            SetMazeStruct(maze);
        }
        public Maze(Cell[][] maze)
        {
            InitMaze();
            SetMazeStruct(maze);
        }
        public Maze(IMazeGenerator generator, int size)
        {
            InitMaze();
            SetMazeStruct(generator, size);
        }

        public void SetExit(MoveDirection direction, int index)
        {
            Cell curCell = null;

            switch(direction)
            {
                case MoveDirection.UP:
                    {
                        curCell = mazeStruct[0, index];

                        curCell.Up = exitCell;
                        exitCell.Down = curCell;

                        break;
                    }
                case MoveDirection.LEFT:
                    {
                        curCell = mazeStruct[index, 0];

                        curCell.Left = exitCell;
                        exitCell.Right = curCell;

                        break;
                    }
                case MoveDirection.DOWN:
                    {
                        curCell = mazeStruct[mazeStruct.Size-1, index];

                        curCell.Down = exitCell;
                        exitCell.Up = curCell;

                        break;
                    }
                case MoveDirection.RIGHT:
                    {
                        curCell = mazeStruct[index, mazeStruct.Size-1];

                        curCell.Right = exitCell;
                        exitCell.Left = curCell;

                        break;
                    }
                default:
                    {
                        throw new Exception("Неизвестное направление");
                    }
            }
        }

        public bool MoveObject(Guid objectID, MoveDirection direction)
        {
            LiveGameObject gameObject = liveObjects.Where(x => x.ObjectID == objectID).First();
            return MoveObject(gameObject, direction);
        }
        public bool MoveObject(LiveGameObject liveGame, MoveDirection direction)
        {
            Cell curcell = mazeStruct[liveGame.CurAddres]; // Достаём ячейку, в которой объект
            return curcell.ReplaceObject(liveGame, direction); // Пытаемся сдвинуть
        }

        public void AddObject(BaseGameObject @object, MazePoint position)
        {
            AddObject(@object, position.Line, position.Column);
        }
        public void AddObject(BaseGameObject @object, int line, int column)
        {
            mazeStruct[line, column].AddObject(@object);
            if (@object is LiveGameObject) liveObjects.Add(@object as LiveGameObject);
        }
        
        public MazeStruct GetMazeStruct() => mazeStruct;

        public MazePoint ExitUp => exitCell.Down.Address;
        public MazePoint ExitDown => exitCell.Up.Address;
        public MazePoint ExitLeft => exitCell.Right.Address;
        public MazePoint ExitRight => exitCell.Left.Address;

        private void InitMaze()
        {
            mazeStruct = null;
            liveObjects = new List<LiveGameObject>();
            MazeID = Guid.NewGuid();

            // TODO : Добавить ячейку выхода
            exitCell = new Cell(int.MaxValue, int.MaxValue);
            //exitCell.AddObject();
        }

        private void SetMazeStruct(MazeStruct maze)
        {
            mazeStruct = new MazeStruct(maze);
        }
        private void SetMazeStruct(Cell[][] maze)
        {
            mazeStruct = new MazeStruct(maze);
        }
        private void SetMazeStruct(IMazeGenerator generator, int size)
        {
            mazeStruct = new MazeStruct(generator.GenerateMazeCells(size));
        }
    }
}
