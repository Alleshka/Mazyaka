// See https://aka.ms/new-console-template for more information
using Maze.Common;
using Maze.GameWorld;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeGenerators;
using System.Text;

int size = 10;


Console.Clear();
var mazeGenerator = new GridMazeGenerator(size, size);
var mazeInfo = mazeGenerator.Generate();
var world = new GameWorld(mazeInfo);

while (true)
{
    var key = Console.ReadKey();

    switch (key.Key)
    {
        case ConsoleKey.W:
            world.Move(MoveDirection.Up);
            break;
        case ConsoleKey.A:
            world.Move(MoveDirection.Left);
            break;
        case ConsoleKey.S:
            world.Move(MoveDirection.Down);
            break;
        case ConsoleKey.D:
            world.Move(MoveDirection.Right);
            break;
    }
}
//PrintMaze(maze);


//void PrintMaze(IMaze maze)
//{
//    var output = new StringBuilder("+");
//    for (int i = 0; i < 10; i++)
//    {
//        var room = maze.GetRoomByCoordinates(0, i);
//        var up = room.GetMazeConnection(MoveDirection.Up).CannPass(room);

//        if (!up)
//        {
//            output.Append("---+");
//        }
//        else
//        {
//            output.Append("    ");
//        }
//    }
//    output.AppendLine();


//    for (int i = 0; i < 10; i++)
//    {
//        var top = maze.GetRoomByCoordinates(i, 0).CanPass(MoveDirection.Left) ? " " : "|";
//        var bottom = "+";

//        for (int j = 0; j < 10; j++)
//        {
//            string body = (i == curRoom.Point.Row && j == curRoom.Point.Column) ? " p " : "   ";

//            var room = maze.GetRoomByCoordinates(i, j);

//            var right = !room.CanPass(MoveDirection.Right);
//            var down = !room.CanPass(MoveDirection.Down);

//            var east = right ? "|" : " ";
//            top += body + east;

//            var south = down ? "---" : "   ";
//            string corner = "+";
//            bottom += south + corner;
//        }
//        output.AppendLine(top);
//        output.AppendLine(bottom);
//    }
//    Console.WriteLine(output.ToString());
//}
internal class CustomStringBuilder
{
    private readonly StringBuilder _builder;

    public CustomStringBuilder(string? value)
    {
        Console.Clear();
        _builder = new StringBuilder(value);
        Console.WriteLine(_builder.ToString());
    }

    public void Append(string? value)
    {
        Console.Clear();
        _builder.Append(value);
        Console.WriteLine(_builder.ToString());
    }

    public void AppendLine()
    {
        Console.Clear();
        _builder.AppendLine();
        Console.WriteLine(_builder.ToString());
    }

    public void AppendLine(string? value)
    {
        Console.Clear();
        _builder.AppendLine(value);
        Console.WriteLine(_builder.ToString());
    }

}