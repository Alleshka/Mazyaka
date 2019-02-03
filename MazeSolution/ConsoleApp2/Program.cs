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
            var generator = new ManualMazeGenerator();
            var structure = new MazeStruct(3, generator);
            Console.WriteLine(GetStructString(structure));
        }

        public static string GetStructString(MazeStruct @struct)
        {
            var builder = new StringBuilder();

            for(int i=0; i<@struct.Size; i++)
            {
                for(int j=0; j<@struct.Size; j++)
                {
                    var cell = @struct[i, j];

                    builder.Append(cell.LeftRelation.CanMove ? "←" : "|");
                    builder.Append("[");


                    if (cell.UpRelation.CanMove && cell.DownRelation.CanMove) builder.Append("↕");
                    else if (cell.UpRelation.CanMove) builder.Append("↑");
                    else if (cell.DownRelation.CanMove) builder.Append("↓");
                    else builder.Append(" ");

                                       
                    builder.Append("]");
                    builder.Append(cell.RightRelation.CanMove ? "→" : "|");
                }

                builder.Append(System.Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
