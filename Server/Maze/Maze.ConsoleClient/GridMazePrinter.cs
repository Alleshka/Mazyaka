using Maze.Common;
using Maze.GameWorld;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;
using System.Text;

namespace Maze.ConsoleClient
{
    record MoveResult(bool Success, int NewRoomId);

    class MazeClientState
    {
        private readonly Dictionary<int, (int x, int y)> roomPos = new();
        private readonly Dictionary<(int x, int y), Cell> cells = new();

        public MazeClientState()
        {
            var startPos = (0, 0);
            roomPos[1] = startPos;
            cells[startPos] = new Cell { IsRoom = true };
        }

        public void RegisterMove(int fromId, MoveDirection dir, int toId)
        {
            if (!roomPos.TryGetValue(fromId, out var from))
                return;

            var delta = GetDelta(dir);
            var toPos = (from.x + delta.dx, from.y + delta.dy);

            roomPos[toId] = toPos;
            cells[toPos] = new Cell { IsRoom = true };

            // Отмечаем, что направление открыто
            cells[from].SetOpen(dir, true);
        }

        public void RegisterBlocked(int fromId, MoveDirection dir)
        {
            if (!roomPos.TryGetValue(fromId, out var from))
                return;

            cells[from].SetOpen(dir, false);
        }

        public (int x, int y)? GetPlayerPos(int roomId) =>
            roomPos.TryGetValue(roomId, out var p) ? p : null;

        public Cell? GetCell(int x, int y) =>
            cells.TryGetValue((x, y), out var c) ? c : null;

        private static (int dx, int dy) GetDelta(MoveDirection d) => d switch
        {
            MoveDirection.Up => (0, -1),
            MoveDirection.Down => (0, 1),
            MoveDirection.Left => (-1, 0),
            MoveDirection.Right => (1, 0),
            _ => (0, 0)
        };
    }

    class Cell
    {
        public bool IsRoom { get; set; } = false;
        public bool[] OpenDirections { get; } = new bool[4]; // 0=N, 1=S, 2=W, 3=E

        public void SetOpen(MoveDirection dir, bool open)
        {
            int idx = dir switch
            {
                MoveDirection.Up => 0,
                MoveDirection.Down => 1,
                MoveDirection.Left => 2,
                MoveDirection.Right => 3,
                _ => -1
            };
            if (idx >= 0) OpenDirections[idx] = open;
        }
    }

    class MazePrinter
    {
        private readonly int radius;

        public MazePrinter(int viewRadius = 6)
        {
            radius = viewRadius;
        }

        public void Print(MazeClientState state, int currentRoomId)
        {
            var center = state.GetPlayerPos(currentRoomId);
            if (!center.HasValue) return;

            int cx = center.Value.x;
            int cy = center.Value.y;

            for (int dy = -radius; dy <= radius; dy++)
            {
                for (int dx = -radius; dx <= radius; dx++)
                {
                    int wx = cx + dx;
                    int wy = cy + dy;

                    var cell = state.GetCell(wx, wy);

                    if (cell == null)
                    {
                        Console.Write(" "); // ничего не известно
                        continue;
                    }

                    if (dx == 0 && dy == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("P");
                        Console.ResetColor();
                        continue;
                    }

                    if (cell.IsRoom)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("#");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n↑W ←A ↓S →D    q — выход");
        }
    }
}
