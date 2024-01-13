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


        public void SetPlayer(int line, int col)
        {
            _curRoom = _curMaze.GetRoomByCoordinates(line, col);
        }

        public MoveResult MovePlayer(Guid userId, MoveDirection direction)
        {
            var mazeSite = _curRoom.GetMazeSite(direction);
            var result = mazeSite.Enter(_player, direction);

            switch (result.Status)
            {
                case MoveStatus.Success:
                    {
                        _curRoom = _curMaze.GetRoomByPoint(result.Point);
                        return result;
                    }
                case MoveStatus.Failure:
                    {
                        return result;
                    }
                default:
                    {
                        return result;
                    }
            }
        }

        public void DestroyRoom(Guid userId, MoveDirection direction)
        {

        }

        public void SetPlayer(MazePoint point)
        {
            SetPlayer(point.Row, point.Column);
        }
    }
}
