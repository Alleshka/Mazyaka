using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public enum DirectionEnum
    {
        Up,
        Right,
        Down,
        Left
    }

    public class DirectionManager
    {
        public static Dictionary<DirectionEnum, IDirection> _directions;

        static DirectionManager()
        {
            _directions = new Dictionary<DirectionEnum, IDirection>();
            _directions.Add(DirectionEnum.Up, new UpDirection());
            _directions.Add(DirectionEnum.Right, new RightDirection());
            _directions.Add(DirectionEnum.Down, new DownDirection());
            _directions.Add(DirectionEnum.Left, new LeftDirection());
        }

        public static IDirection GetDirection(DirectionEnum @enum)
        {
            if (_directions.TryGetValue(@enum, out IDirection direction))
            {
                return direction;
            }
            else throw new Exception($"Неизвестное направление {@enum}");
        }

    }
}
