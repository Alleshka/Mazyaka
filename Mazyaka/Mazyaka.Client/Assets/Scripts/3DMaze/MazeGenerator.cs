using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assets.Scripts._3DMaze
{
    public class MazeGenerator
    {
        private const int Base = 10;
        private readonly float _scale;

        private static readonly int[] dx = { 0, 0, -1, 1, 0, 0 };
        private static readonly int[] dy = { -1, 1, 0, 0, 0, 0 };
        private static readonly int[] dz = { 0, 0, 0, 0, -1, 1 };

        private readonly Random _random = new Random();

        [Flags]
        public enum Flags
        {
            WALL_LEFT = 1,
            WALL_RIGHT = 2,
            WALL_DOWN = 4,
            WALL_UP = 8,
            WALL_BELOW = 16,
            WALL_ABOVE = 32,
            SOLID = WALL_ABOVE | WALL_BELOW | WALL_LEFT | WALL_RIGHT | WALL_UP | WALL_DOWN,
            VISITED = 64
        }

        public MazeGenerator(float scale = 2)
        {
            _scale = scale;
        }

        public int RandomDirection(Random random, float scale)
        {
            int scaled = (int)(scale * Base);
            var next = random.Next(1, 4 * scaled + 2 * Base + 1);
            if (next / scaled < 4) next = (int)Math.Ceiling((double)next / scaled);
            else next = (int)Math.Ceiling((next - 4 * scaled) / (double)Base) + 4;
            return next - 1;
        }

        public Flags[,,] GenerateMaze(int xSize, int ySize, int zSize)
        {
            if (xSize == 0 || ySize == 0 || zSize == 0) throw new ArgumentException("One or more parameters are equal to 0");
            Flags[,,] maze = new Flags[xSize, ySize, zSize];
            // Current cell = start cell (chosen randomly)
            Cell currentCell = new Cell(_random.Next(xSize), _random.Next(ySize), _random.Next(zSize));
            Stack<Cell> stack = new Stack<Cell>();

            InitializeMaze(maze);
            maze[currentCell.X, currentCell.Y, currentCell.Z] |= Flags.VISITED;

            // While there are unvisited cells
            while (true)
            {
                int direction = 0;
                Flags visited = 0;
                while (visited < Flags.SOLID)
                {
                    // Choose randomly one of the unvisited neighbours
                    direction = RandomDirection(_random, _scale);

                    if ((visited & (Flags)Math.Pow(2, direction)) != 0) continue;
                    visited |= (Flags)Math.Pow(2, direction);
                    if (dx[direction] + currentCell.X < 0 || dx[direction] + currentCell.X >= xSize ||
                        dy[direction] + currentCell.Y < 0 || dy[direction] + currentCell.Y >= ySize ||
                        dz[direction] + currentCell.Z < 0 || dz[direction] + currentCell.Z >= zSize)
                        continue;
                    if ((maze[currentCell.X + dx[direction], currentCell.Y + dy[direction],
                             currentCell.Z + dz[direction]] & Flags.VISITED) == 0)
                        break;
                }

                if (visited == Flags.SOLID)
                {
                    if (stack.Count != 0) currentCell = stack.Pop();
                    else break;
                }
                else
                {
                    // Push the current cell to the stack
                    stack.Push(currentCell);

                    // Remove the wall between the current cell
                    maze[currentCell.X, currentCell.Y, currentCell.Z] ^= (Flags)Math.Pow(2, direction);

                    // Make the chosen cell the current cell and mark it as visited
                    currentCell = new Cell(currentCell.X + dx[direction], currentCell.Y + dy[direction], currentCell.Z + dz[direction]);
                    maze[currentCell.X, currentCell.Y, currentCell.Z] |= Flags.VISITED;

                    // Changing to opposite direction for removing wall from chosen cell
                    if (direction % 2 == 0) direction++;
                    else direction--;
                    maze[currentCell.X, currentCell.Y, currentCell.Z] ^= (Flags)Math.Pow(2, direction);
                }
            }
            return maze;
        }

        /// <summary>
        /// Gets empty maze with no walls, except for outer
        /// </summary>
        /// <returns>empty maze</returns>
        public static Flags[,,] GetEmptyMaze(int xSize, int ySize, int zSize)
        {
            Flags[,,] maze = new Flags[xSize, ySize, zSize];

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    for (int z = 0; z < zSize; z++)
                    {
                        if (x == 0)
                            maze[x, y, z] |= Flags.WALL_DOWN;
                        else if(x == xSize - 1)
                            maze[x, y, z] |= Flags.WALL_UP;
                        if (y == 0)
                            maze[x, y, z] |= Flags.WALL_LEFT;
                        else if (y == ySize - 1)
                            maze[x, y, z] |= Flags.WALL_RIGHT;
                        if (z == 0)
                            maze[x, y, z] |= Flags.WALL_BELOW;
                        if (z == zSize - 1)
                            maze[x, y, z] |= Flags.WALL_ABOVE;
                        
                    }
                }
            }

            return maze;
        }

        private static void InitializeMaze(Flags[,,] maze)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    for (int z = 0; z < maze.GetLength(2); z++)
                    {
                        maze[x, y, z] = Flags.SOLID;
                    }
                }
            }
        }

        private struct Cell
        {
            public readonly int X;
            public readonly int Y;
            public readonly int Z;

            public Cell(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
