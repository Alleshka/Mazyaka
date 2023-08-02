// See https://aka.ms/new-console-template for more information
using Maze.MazeStructure;
using Maze.MazeStructure.Builder;
using System.Text;
using static System.Console;

var mazeBuilder = new SimpleMazeBuilder();
var mazeGame = new SimpleMazeGame();
var maze = mazeGame.CreateMaze(new RecursiveMazeGenerator(), mazeBuilder);

var output = new StringBuilder("+");
for (int i = 0; i < 10; i++)
{
    output.Append("---+");
}
output.AppendLine();


for (int i = 0; i < 10; i++)
{
    var top = "|";
    var bottom = "+";

    for (int j = 0; j < 10; j++)
    {
        string body = "   ";

        var room = maze.GetRoomByCoordinates(i, j);

        var left = room.GetMazeSite(Maze.Common.MoveDirection.Left) is IMazeWall;
        var up = room.GetMazeSite(Maze.Common.MoveDirection.Up) is IMazeWall;
        var right = room.GetMazeSite(Maze.Common.MoveDirection.Right) is IMazeWall;
        var down = room.GetMazeSite(Maze.Common.MoveDirection.Down) is IMazeWall;

        var east = right ? "|" : " ";
        top += body + east;

        var south = down ? "---" : "   ";
        string corner = "+";
        bottom += south + corner;
    }
    output.AppendLine(top);
    output.AppendLine(bottom);
}

Console.WriteLine(output.ToString());