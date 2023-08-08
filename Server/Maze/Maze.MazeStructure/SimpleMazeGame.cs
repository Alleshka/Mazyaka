using Maze.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public class SimpleMazeGame : IMazeGame
    {
        private IMazeRoom _curRoom;
        private IMaze _curMaze;
        private IMazePlayer _player;

        public SimpleMazeGame()
        {
            _player = new SimpleMazePlayer();
        }

        public IMaze CreateMaze(IMazeGenerator generator, IMazeBuilder mazeBuilder)
        {
            _curMaze = generator.GenerateMaze(mazeBuilder);
            return _curMaze;
        }

        public IMaze GetMaze => _curMaze;

        public void SetPlayer(int line, int col)
        {
            _curRoom = _curMaze.GetRoomByCoordinates(line, col);
            _player.Line = line;
            _player.Col = col;
        }

        public MoveResult MovePlayer(MoveDirection direction)
        {
            var otherCell = _curRoom.GetMazeSite(direction);
            var result = otherCell.Enter(_player);
            _curRoom = _curMaze.GetRoomByCoordinates(_player.Line, _player.Col);
            return result;
        }
    }
}
