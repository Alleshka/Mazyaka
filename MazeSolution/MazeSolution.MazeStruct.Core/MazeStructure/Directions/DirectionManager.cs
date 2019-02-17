﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    [Flags]
    public enum DirectionEnum
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }

    public class DirectionManager
    {
        public static Dictionary<DirectionEnum, IDirection> _directions;

        static DirectionManager()
        {
            _directions = new Dictionary<DirectionEnum, IDirection>
            {
                { DirectionEnum.Up, new UpDirection() },
                { DirectionEnum.Right, new RightDirection() },
                { DirectionEnum.Down, new DownDirection() },
                { DirectionEnum.Left, new LeftDirection() }
            };
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
