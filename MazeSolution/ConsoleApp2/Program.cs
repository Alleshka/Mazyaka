using MazeSolution.MazeStruct.Core.MazeStructure;
using MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators;
using System;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var generator = new RecursiveMazeGenerator();
            var structure = new MazeStructureClass(5, generator);
            Console.WriteLine(GetStructString(structure));
        }

        public static string GetStructString(MazeStructureClass @struct)
        {
            var builder = new StringBuilder();

            for(int i=0; i<@struct.Size; i++)
            {
                for(int j=0; j<@struct.Size; j++)
                {
                    var cell = @struct[i, j];

                    builder.Append(cell[MazeSolution.MazeStruct.Core.MazeStructure.Directions.DirectionEnum.Left].CanMove ? "←" : "|");
                    builder.Append("[");


                    if (cell[MazeSolution.MazeStruct.Core.MazeStructure.Directions.DirectionEnum.Up].CanMove && cell[MazeSolution.MazeStruct.Core.MazeStructure.Directions.DirectionEnum.Down].CanMove) builder.Append("↕");
                    else if (cell[MazeSolution.MazeStruct.Core.MazeStructure.Directions.DirectionEnum.Up].CanMove) builder.Append("↑");
                    else if (cell[MazeSolution.MazeStruct.Core.MazeStructure.Directions.DirectionEnum.Down].CanMove) builder.Append("↓");
                    else builder.Append(" ");

                                       
                    builder.Append("]");
                    builder.Append(cell[MazeSolution.MazeStruct.Core.MazeStructure.Directions.DirectionEnum.Right].CanMove ? "→" : "|");
                }

                builder.Append(System.Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
