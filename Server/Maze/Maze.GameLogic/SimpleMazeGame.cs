using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.GameLogic
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


        public Result SetPlayer(int line, int col)
        {
            _curRoom = _curMaze.GetRoomByCoordinates(line, col);
            _player.Line = line;
            _player.Col = col;

            return Result.Success();
        }

        public Result<MoveResult> MovePlayer(Guid userId, MoveDirection direction)
        {
            var otherCell = _curRoom.GetMazeSite(direction);
            var result = otherCell.Enter(_player);
            _curRoom = _curMaze.GetRoomByCoordinates(_player.Line, _player.Col);
            return Result.Success(result);
        }

        public Result DestroyRoom(Guid userId, MoveDirection direction)
        {
            var userRoom = _curMaze.GetRoomByCoordinates(_player.Line, _player.Col);

            throw new NotImplementedException();
        }

        public Result SetPlayer(MazePoint point)
        {
            throw new NotImplementedException();
        }
    }
}
