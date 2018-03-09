using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Mazyaka.MazeGeneral.MazeGenerator;

namespace Mazyaka.MazeGeneral.MazeModel
{
    /// <summary>
    /// Игровое поле
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(MazeStruct))]
    [KnownType(typeof(Cell))]
    [KnownType(typeof(Point))]
    [KnownType(typeof(Exit))]
    [KnownType(typeof(FalseKey))]
    [KnownType(typeof(Key))]
    [KnownType(typeof(Human))]
    [KnownType(typeof(Minotaur))]
    public class MazeArea : TransmittedClass<MazeArea>
    {
        [DataMember]
        public Guid MazeID { get; private set; }
        [DataMember]
        private MazeStruct _structLab = null; // Струкура лабиринта
        [DataMember]
        private Cell _exitCell; // Ячейка выхода
        [DataMember]
        private List<GameObject> _objectList; // Игровые объекты в лабиринте

        public Cell this[Point point] => _structLab[point.Line, point.Column];
        public Cell this[int line, int column] => _structLab[line, column];

        public MazeArea()
        {
            MazeID = Guid.NewGuid();

            _objectList = new List<GameObject>();

            // Создание ячейки с выходом
            _exitCell = new Cell(int.MaxValue, int.MaxValue);
            _exitCell.Add(new Exit());
        }

        // Задать структуру лабиринта снаружи
        public void SetStructLab(MazeStruct maze)
        {
            _structLab = maze;
        }

        /// <summary>
        /// Создать сгенерированный лабиринт
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="size"></param>
        public void GeerateStructLab(ILabirintGenerator generator, int size)
        {
            _structLab = generator.Generate(size);
        }

        public void AddGameObject(GameObject obj)
        {
            if (obj != null)
            {
                _objectList.Add(obj); // ДОбавляем в список объектов лабиринта
                _structLab[obj.Position].Content.Add(obj); // Добавляем в ячейку
            }
            else throw new Exception("Отсутствует лабиринт");
        }

        public void SetExit(MoveDirection direction, int cell)
        {
            // TODO: тут должна стоять проверка на попытку добавления нескольких входов в ряд

            switch (direction)
            {
                case MoveDirection.UP:
                    {
                        _structLab[0, cell].UP = _exitCell;
                        _exitCell.DOWN = _structLab[0, cell];
                        break;
                    }
                case MoveDirection.LEFT:
                    {
                        _structLab[cell, 0].LEFT = _exitCell;
                        _exitCell.RIGHT = _structLab[cell, 0];
                        break;
                    }
                case MoveDirection.DONW:
                    {
                        _structLab[_structLab.Size - 1, cell].DOWN = _exitCell;
                        _exitCell.UP = _structLab[_structLab.Size - 1, cell];
                        break;
                    }
                case MoveDirection.RIGHT:
                    {
                        _structLab[cell, _structLab.Size - 1].RIGHT = _exitCell;
                        _exitCell = _structLab[cell, _structLab.Size - 1];
                        break;
                    }
            }
        }

        // TODO : Слишком наглый метод
        /// <summary>
        /// Передвигает объект с указанным ID в указанном направлении
        /// </summary>
        /// <param name="idObject"></param>
        /// <param name="direction"></param>
        /// <returns>Если передвижение успешное - true, иначе - false </returns>
        public bool MoveLiveObject(Guid idObject, MoveDirection direction)
        {
            GameObject curObject = GetObject(idObject); // Находим указанный объект 

            if (curObject is LiveGameObject)
            {
                Cell curCell = null;
                Cell newCell = null;

                curCell = _structLab[curObject.Position]; // Текущая ячейка
                newCell = GetNewCell(direction, curCell); // Достаём целевую ячейку

                if (newCell != null)
                {
                    // Сначала выполняются действия объетов
                    foreach (var item in newCell.Content)
                    {
                        item.Action(curObject);
                    }

                    // Меняем содержимое ячеек
                    newCell.Add(curObject);
                    curCell.Content.Remove(curObject);

                    curObject.Position = newCell.Address; // Меняем адрес у объекта

                    return true; // Всё прошло успешно
                }
                else return false; // Передвинуь невозможно

            }
            else throw new Exception("Данный объект невозможно переместить");
        }

        private GameObject GetObject(Guid idObject)
        {
            return _objectList.Where(x => x.ID == idObject).First();
        }

        private Cell GetNewCell(MoveDirection direction, Cell curCell)
        {
            switch (direction)
            {
                case MoveDirection.DONW:
                    return curCell.DOWN;
                case MoveDirection.LEFT:
                    return curCell.LEFT;
                case MoveDirection.RIGHT:
                    return curCell.RIGHT;
                case MoveDirection.UP:
                    return curCell.UP;
                default: throw new Exception("Неизвестное направление");
            }
        }
    }
}