using Maze.Common;
using Microsoft.AspNetCore.SignalR;

namespace Maze.Server.SignalR
{
    public class GameHub : Hub
    {
        private static Random T = new Random();

        public async Task Send (MoveDirection direction)
        {
            string result;
            
            switch(direction)
            {
                case MoveDirection.Left:
                    {
                        result = "Left";
                        break;
                    }
                case MoveDirection.Right:
                    {
                        result = "Right";
                        break;
                    }
                case MoveDirection.Down:
                    {
                        result = "Down";
                        break;
                    }
                case MoveDirection.Up:
                    {
                        result = "UP";
                        break;
                    }
                default:
                    {
                        result = @"¯\_(ツ)_/¯";
                        break;
                    }
            }

            await Clients.All.SendAsync("Send", result);
        }

        public async Task Move(MoveDirection direction)
        {
            string t = ("Move to " + direction + " cell");
            Console.WriteLine(t);
            await Task.FromResult(0);
            string res = T.NextDouble() > 0.5 ? "Success" : "Wall";
            await Clients.All.SendAsync("MoveResult", t + "::" + res);
        }
    }
}
