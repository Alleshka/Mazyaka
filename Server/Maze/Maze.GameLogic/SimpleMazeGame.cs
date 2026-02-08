using Maze.Core;
using Maze.MazeStructure.Interfaces;
using Maze.MazeStructure;
using Maze.MazeStructure.Builder;
using Maze.Core.Common;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic
{
    public class SimpleMazeGame : IMazeGame
    {
        private IMazeRoom _curRoom;
        private IMaze _curMaze;
        private IMazePlayer _player;

        private int minVisibleRow = 0;
        private int minVisibleCol = 0;
        private int maxVisibleRow = 0;
        private int maxVisibleCol = 0;

        public SimpleMazeGame()
        {
            _player = new SimpleMazePlayer();
        }

        public void SetMaze(IMaze maze)
        {
            _curMaze = maze;
        }

        public IMaze GetMaze => _curMaze;


        public IMazeRoom SetPlayer(int line, int col)
        {
            _curRoom = _curMaze.GetRoomByCoordinates(line, col);

            minVisibleRow = maxVisibleRow = line;
            minVisibleCol = maxVisibleCol = col;

            return _curRoom;
        }

        public MoveResult MovePlayer(Guid userId, MoveDirection direction)
        {
            var mazeSite = _curRoom.GetMazeConnection(direction);
            var result = mazeSite.TryEnter(_player, _curRoom);    
            Console.WriteLine($"{result.IsSuccess}: {result.FailureReason} ({result.Message})");

            if (result.IsSuccess)
            {
                _curRoom = result.NextRoom;

                minVisibleCol = Math.Min(minVisibleCol, _curRoom.Point.Column);
                maxVisibleCol = Math.Max(maxVisibleCol, _curRoom.Point.Column);

                minVisibleRow = Math.Min(minVisibleRow, _curRoom.Point.Row);
                maxVisibleRow = Math.Max(maxVisibleRow, _curRoom.Point.Row);
            }

            return result;
        }

        public IMazeRoom SetPlayer(MazePoint point)
        {
            return SetPlayer(point.Row, point.Column);
        }

        public IEnumerable<IMazeRoom> GetVisibleRooms()
        {
            for (int i = minVisibleRow; i <= maxVisibleRow; i++)
            {
                for (int j = minVisibleCol; j <= maxVisibleCol; j++)
                {
                    yield return _curMaze.GetRoomByCoordinates(i, j);
                }
            }
        }
    }
}
