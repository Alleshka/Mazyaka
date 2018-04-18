using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public interface IActCommand
    {
        void Execute();
    }

    public abstract class ActAbstract : IActCommand
    {
        protected AbstractRequest request;
        protected AbstractResponse response;

        public ActAbstract(AbstractRequest req)
        {
            request = req;
        }

        public abstract void Execute();
    }

    public abstract class ActGameAbstract : ActAbstract
    {
        protected IGameService gameService;

        public ActGameAbstract(AbstractRequest message, IGameService game) : base(message)
        {
            gameService = game;
        }
    }
}
