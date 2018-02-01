using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mazyaka.Model.StructLabirint;
using Mazyaka.Model.GameObjects;
using Mazyaka.Model.LabirintGenerator;

namespace Mazyaka.Model
{
    // TODO : Этот класс себе много позволяет. Надо вынести валидацию в отдельные методы
    /// <summary>
    /// Игровой лабиринт
    /// Содержит свою структуру, передвигает объекты
    /// </summary>
    public class GameLabirint
    {
        public StructLabirint.StructLabirint @struct; // Содержит информацию о структуре лабиринта
        public Cell @exitCell; // Ячейка выхода 

        public List<GameObject> @gameObjects; // Содержит информацию о всех игровых объектах в лабиринте

        public GameLabirint()
        {
            gameObjects = new List<GameObject>();

            exitCell = new Cell(int.MaxValue, int.MaxValue); // Создаём ячейку выхода
            exitCell.Add(new Exit()); // Ставим в неё выход

            @struct = null;
        }

        /// <summary>
        /// Добавить игровой объект в лабиринт и соответствующую ячейку
        /// </summary>
        /// <param name="obj"></param>
        public void AddGameObject(GameObject obj)
        {
            gameObjects.Add(obj); // Добавляем в список всех объектов
            @struct[obj.Position].Add(obj); // Добавляем в ячейку
        }

        /// <summary>
        /// Установить лабиринт
        /// </summary>
        /// <param name="labirint"></param>
        public void SetLabirintStruct(StructLabirint.StructLabirint labirint)
        {
            @struct = labirint;  
        }
        //public void SetLabirintStruct()
        //{
        //    @struct = new StructLabirint.StructLabirint();
        //    @struct.GenerateLabirint(new RecursiveGenerator());
        //}

        //// Установить выходы на границах
        //// TODO : Пока случайным образом
        //public void PutExitCell()
        //{
        //    Random T = new Random();
        //    int t = T.Next(@struct.Size);

        //    @struct[0, t].UP = exitCell;
        //    exitCell.DOWN = @struct[0, t];

        //    @struct[t, 0].LEFT = exitCell;
        //    exitCell.RIGHT = @struct[t, 0];

        //    @struct[@struct.Size - 1, t].DOWN = exitCell;
        //    exitCell.UP = @struct[@struct.Size - 1, t];

        //    @struct[t, @struct.Size - 1].RIGHT = exitCell;
        //    exitCell.LEFT = @struct[t, @struct.Size - 1];
        //}

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

                curCell = @struct[curObject.Position]; // Текущая ячейка
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
            return gameObjects.Where(x => x.ID == idObject).First();
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