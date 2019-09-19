using MazeSolution.Core.MazeStructrure;
using System;

namespace MazeSolution.Core.Generators
{
    /// <summary>
    /// Интерфейс генератора лабиринта
    /// </summary>
    public interface IMazeGenerator
    {
        /// <summary>
        /// Сгенерировать структуру лабиринта
        /// </summary>
        /// <param name="lineCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        /// <returns></returns>
        IMazeStructure GenerateMaze(int lineCount, int columnCount);
    }
}
